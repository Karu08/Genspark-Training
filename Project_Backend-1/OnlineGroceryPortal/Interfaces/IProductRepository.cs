using OnlineGroceryPortal.Models;

namespace OnlineGroceryPortal.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);

        Task<Product?> GetByIdAsync(long id);
        Task<List<Product>> GetByTypeAsync(string type);

        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}