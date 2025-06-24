using Moq;
using NUnit.Framework;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Services;
using OnlineGroceryPortal.Contexts;
using Microsoft.EntityFrameworkCore;
using OnlineGroceryPortal.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using OnlineGroceryPortal.Services.Misc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OnlineGroceryPortal.Controllers;

namespace OnlineGroceryPortal.Test
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private GroceryDbContext _context;
        private ProductService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();

            long uniqueId = DateTime.UtcNow.Ticks;
            var options = new DbContextOptionsBuilder<GroceryDbContext>()
                .UseInMemoryDatabase(uniqueId.ToString()) 
                .Options;

            _context = new GroceryDbContext(options);
            _service = new ProductService(_mockRepo.Object, _context);
        }

        [Test]
        public async Task GetAllProductsAsync_ReturnsMappedProductDtos()
        {
            var fakeProducts = new List<Product>
            {
                new Product { Id = 1L, ProdName = "Tomato", Price = 30 },
                new Product { Id = 2L, ProdName = "Potato", Price = 40 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProducts);

            var result = await _service.GetAllProductsAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Tomato"));
            Assert.That(result[1].Price, Is.EqualTo(40));
        }

        [Test]
        public async Task AddProductAsync_ShouldAddAndReturnProductDto()
        {
            var dto = new CreateProductDto
            {
                Name = "Potato",
                Description = "Fresh from the farm",
                Type = "Vegetable",
                Price = 90,
                Stock = 10
            };

            var result = await _service.AddProductAsync(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Price, Is.EqualTo(dto.Price));

            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProdName == dto.Name);
            Assert.That(dbProduct, Is.Not.Null);
            Assert.That(dbProduct.Description, Is.EqualTo(dto.Description));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }

    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _mockRepo;
        private GroceryDbContext _context;
        private OrderService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IOrderRepository>();

            long uniqueId = DateTime.UtcNow.Ticks;
            var options = new DbContextOptionsBuilder<GroceryDbContext>()
                .UseInMemoryDatabase(uniqueId.ToString()) 
                .Options;

            _context = new GroceryDbContext(options);
            _service = new OrderService(_mockRepo.Object, _context);
        }

        // [Test]
        // public async Task PlaceOrderAsync_ReturnsOrder()
        // {
        //     var customerId = 1234L;
        //     var dto = new OrderDto
        //     {
        //         CustomerId = customerId,
        //         Items = new List<OrderItemDto>
        //         {
        //             new OrderItemDto { ProductId = 1L, Quantity = 1 }
        //         }
        //     };

        //     _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Order>()))
        //              .ReturnsAsync(new Order { CustomerId = customerId });

        //     var result = await _service.PlaceOrderAsync(customerId, dto);

        //     Assert.That(result, Is.Not.Null);
        //     Assert.That(result.CustomerId, Is.EqualTo(customerId));
        // }

        [Test]
        public async Task GetOrderStatusAsync_ReturnsStatus()
        {
            long orderId = 1001L; 
            _mockRepo.Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(new Order { Id = orderId, Status = "Pending" });

            var status = await _service.GetOrderStatusAsync(orderId);

            Assert.That(status, Is.EqualTo("Pending"));
        }


        [Test]
        public async Task UpdateOrderStatusAsync_UpdatesStatus()
        {
            var orderId = 1002L;
            var order = new Order { Id = orderId, Status = "Pending" };

            _mockRepo.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockRepo.Setup(r => r.UpdateAsync(order)).Returns(Task.CompletedTask);

            var result = await _service.UpdateOrderStatusAsync(orderId, "Delivered");

            Assert.That(result, Is.True);
            Assert.That(order.Status, Is.EqualTo("Delivered"));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }

    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockService;
        private Mock<IHubContext<OrderHub>> _mockHubContext;
        private Mock<IClientProxy> _mockClientProxy;
        private OrderController _controller;


        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IOrderService>();
            _mockClientProxy = new Mock<IClientProxy>();
            var mockClients = new Mock<IHubClients>();
            mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);

            _mockHubContext = new Mock<IHubContext<OrderHub>>();
            _mockHubContext.Setup(hub => hub.Clients).Returns(mockClients.Object);

            _controller = new OrderController(_mockService.Object, _mockHubContext.Object);

            // Setting mock user with claims (Role = Agent)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Agent"), 
                new Claim(ClaimTypes.NameIdentifier, 1234L.ToString())
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

        }


        [Test]
        public async Task TrackOrderStatus_ReturnsStatus()
        {
                long orderId = 1234L;

            _mockService.Setup(s => s.GetOrderStatusAsync(orderId)).ReturnsAsync("Pending");

            var result = await _controller.TrackOrderStatus(orderId) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateOrderStatus_ReturnsOk()
        {
                long orderId = 1234L;

            _mockService.Setup(s => s.UpdateOrderStatusAsync(orderId, "Delivered")).ReturnsAsync(true);

            var dto = new UpdateOrderStatusDto { Status = "Delivered" };
            var result = await _controller.UpdateOrderStatus(orderId, dto) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }

    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<ITokenService> _mockTokenService;
        private Mock<IPasswordHasher> _mockPwdHasher;
        private AuthenticationService _service;

        [SetUp]
        public void Setup()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _mockPwdHasher = new Mock<IPasswordHasher>();

            _service = new AuthenticationService(
                _mockUserRepo.Object,
                _mockTokenService.Object,
                _mockPwdHasher.Object
            );
        }

        [Test]
        public async Task RegisterAsync_ShouldCreateUserAndReturnToken()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Name = "John",
                Username = "john123",
                Password = "password",
                Email = "john@example.com",
                Role = "Customer",
                PhoneNum = "1234567890"
            };

            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(registerDto.Username))
                         .ReturnsAsync((User?)null);

            _mockPwdHasher.Setup(p => p.HashPassword(registerDto.Password))
                          .Returns("hashed_pw");

            _mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
             .ReturnsAsync((User u) => u);


            _mockTokenService.Setup(t => t.GenerateToken(It.IsAny<User>()))
                             .Returns(new TokenDto { AccessToken = "access", RefreshToken = "refresh" });

            // Act
            var result = await _service.RegisterAsync(registerDto);

            // Assert
            Assert.That(result.AccessToken, Is.EqualTo("access"));
            Assert.That(result.RefreshToken, Is.EqualTo("refresh"));
        }

        [Test]
        public void RegisterAsync_ThrowsException_IfUsernameExists()
        {
            // Arrange
            var dto = new RegisterDto { Username = "existing" };

            _mockUserRepo.Setup(r => r.GetByUsernameAsync(dto.Username))
                         .ReturnsAsync(new User());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _service.RegisterAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Username already exists!"));
        }

        [Test]
        public async Task LoginAsync_ShouldReturnToken_IfValidCredentials()
        {
            var dto = new LoginDto { Username = "john123", Password = "password" };

            var user = new User { Username = "john123", PasswordHash = "hashed_pw" };

            _mockUserRepo.Setup(r => r.GetByUsernameAsync(dto.Username)).ReturnsAsync(user);
            _mockPwdHasher.Setup(p => p.VerifyPassword(dto.Password, user.PasswordHash)).Returns(true);

            _mockTokenService.Setup(t => t.GenerateToken(user))
                             .Returns(new TokenDto { AccessToken = "token", RefreshToken = "refresh" });

            var result = await _service.LoginAsync(dto);

            Assert.That(result.AccessToken, Is.EqualTo("token"));
        }

        [Test]
        public void LoginAsync_InvalidCredentials_ThrowsException()
        {
            var dto = new LoginDto { Username = "john", Password = "wrong" };

            _mockUserRepo.Setup(r => r.GetByUsernameAsync(dto.Username)).ReturnsAsync((User?)null);

            var ex = Assert.ThrowsAsync<Exception>(() => _service.LoginAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Invalid credentials"));
        }

        [Test]
        public async Task RefreshTokenAsync_ShouldReturnNewToken_IfTokenValid()
        {
            var user = new User { Username = "john" };
            _mockUserRepo.Setup(r => r.GetByRefreshTokenAsync("valid")).ReturnsAsync(user);

            _mockTokenService.Setup(t => t.GenerateToken(user))
                             .Returns(new TokenDto { AccessToken = "newAccess", RefreshToken = "newRefresh" });

            var result = await _service.RefreshTokenAsync("valid");

            Assert.That(result.AccessToken, Is.EqualTo("newAccess"));
        }

        [Test]
        public void RefreshTokenAsync_InvalidToken_ThrowsException()
        {
            _mockUserRepo.Setup(r => r.GetByRefreshTokenAsync("invalid")).ReturnsAsync((User?)null);

            var ex = Assert.ThrowsAsync<Exception>(() => _service.RefreshTokenAsync("invalid"));
            Assert.That(ex!.Message, Is.EqualTo("Invalid Refresh Token"));
        }
    }
    
   
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _mockService;
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkWithProducts()
        {
            // Arrange
            var fakeProducts = new List<ProductDto>
            {
                new ProductDto { Id = 101L, Name = "Tomato" },
                new ProductDto { Id = 102L, Name = "Potato" }
            };
            _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(fakeProducts);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(fakeProducts));
        }

        [Test]
        public async Task GetById_ReturnsOk_WhenProductFound()
        {
            long productId = 1234L;
            var product = new ProductDto { Id = productId, Name = "Apple" };
            _mockService.Setup(s => s.GetProductByIdAsync(productId)).ReturnsAsync(product);

            var result = await _controller.GetById(productId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_ReturnsNotFound_WhenProductNotFound()
        {
            _mockService.Setup(s => s.GetProductByIdAsync(It.IsAny<long>())).ReturnsAsync((ProductDto?)null);

            var result = await _controller.GetById(999L); // use a dummy long ID

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }


        [Test]
        public async Task GetByType_ReturnsOkWithProducts()
        {
            var type = "Fruit";
            var products = new List<ProductDto> { new ProductDto { Name = "Banana", Type = type } };
            _mockService.Setup(s => s.GetProductsByTypeAsync(type)).ReturnsAsync(products);

            var result = await _controller.GetByType(type);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var dto = new CreateProductDto { Name = "Orange", Type = "Fruit", Price = 10 };
            var created = new ProductDto { Id = 1234L, Name = "Orange" };
            _mockService.Setup(s => s.AddProductAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task Update_ReturnsOk_WhenSuccessful()
        {
            long id = 1234L;
            var dto = new UpdateProductDto { Name = "Updated Orange", Price = 20 };
            var updated = new ProductDto { Id = id, Name = "Updated Orange" };
            _mockService.Setup(s => s.UpdateProductAsync(id, dto)).ReturnsAsync(updated);

            var result = await _controller.Update(id, dto);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Update_ReturnsNotFound_OnError()
        {
            long id = 1234L;
            var dto = new UpdateProductDto { Name = "Invalid" };
            _mockService.Setup(s => s.UpdateProductAsync(id, dto))
                        .ThrowsAsync(new Exception("Product not found"));

            var result = await _controller.Update(id, dto);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            long id = 1234L;
            _mockService.Setup(s => s.DeleteProductAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Delete_ReturnsNotFound_OnError()
        {
            long id = 1234L;
            _mockService.Setup(s => s.DeleteProductAsync(id)).ThrowsAsync(new Exception("Product not found"));

            var result = await _controller.Delete(id);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetPagedProducts_ReturnsOkResult()
        {
            var pageNumber = 1;
            var pageSize = 5;
            var resultList = new List<ProductDto>
            {
                new ProductDto { Name = "Rice" },
                new ProductDto { Name = "Wheat" }
            };

            _mockService.Setup(s => s.GetPagedProductsAsync(pageNumber, pageSize)).ReturnsAsync(resultList);

            var result = await _controller.GetPagedProducts(pageNumber, pageSize);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}

