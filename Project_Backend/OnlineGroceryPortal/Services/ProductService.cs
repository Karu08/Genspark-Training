using OnlineGroceryPortal.Repositories;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Contexts;
using Microsoft.EntityFrameworkCore;


namespace OnlineGroceryPortal.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly GroceryDbContext _context;

        public ProductService(IProductRepository repo, GroceryDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.ProdName,
                Description = p.Description,
                Type = p.Type,
                Price = p.Price,
                Stock = p.StockQty
            }).ToList();
        }


        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.ProdName,
                Description = product.Description,
                Type = product.Type,
                Price = product.Price,
                Stock = product.StockQty
            };
        }


        public async Task<List<ProductDto>> GetProductsByTypeAsync(string type)
        {
            var products = await _repo.GetByTypeAsync(type);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.ProdName,
                Description = p.Description,
                Type = p.Type,
                Price = p.Price,
                Stock = p.StockQty
            }).ToList();
        }


        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProdName = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                Price = dto.Price,
                StockQty = dto.Stock
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
            Id = product.Id,
            Name = product.ProdName,
            Description = product.Description,
            Type = product.Type,
            Price = product.Price,
            Stock = product.StockQty
            };

        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            product.ProdName = dto.Name;
            product.Price = dto.Price;

            var updated = await _repo.UpdateAsync(product);

            return new ProductDto
            {
            Id = product.Id,
            Name = product.ProdName,
            Description = product.Description,
            Type = product.Type,
            Price = product.Price,
            Stock = product.StockQty
            };
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            await _repo.DeleteAsync(product);
        }

        public async Task<List<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize)
{
    var products = await _context.Products
        .OrderBy(p => p.ProdName)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return products.Select(p => new ProductDto
    {
        Id = p.Id,
        Name = p.ProdName,
        Description = p.Description,
        Type = p.Type,
        Price = p.Price,
        Stock = p.StockQty
    }).ToList();
}


    }
}
