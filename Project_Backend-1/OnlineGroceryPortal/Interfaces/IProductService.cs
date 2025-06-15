using OnlineGroceryPortal.Models.DTOs;

namespace OnlineGroceryPortal.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(long id);
        Task<List<ProductDto>> GetProductsByTypeAsync(string type);

        Task<ProductDto> AddProductAsync(CreateProductDto dto);
        Task<ProductDto> UpdateProductAsync(long id, UpdateProductDto dto);
        Task DeleteProductAsync(long id);
        Task<List<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize);


    }
}