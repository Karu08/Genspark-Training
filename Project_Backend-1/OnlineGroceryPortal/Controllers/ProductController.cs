using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineGroceryPortal.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all products"
        )]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get products by ID"
        )]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpGet("type/{type}")]
        [SwaggerOperation(
            Summary = "Get products by Category"
        )]
        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByType(string type)
        {
            var products = await _service.GetProductsByTypeAsync(type);
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            Console.WriteLine(">>>>> REACHED POST /api/products endpoint");
            var product = await _service.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var updatedProduct = await _service.UpdateProductAsync(id, dto);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _service.GetPagedProductsAsync(pageNumber, pageSize);
            return Ok(result);
        }

    }
}
