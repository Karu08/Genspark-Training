using OnlineGroceryPortal.Contexts;
using OnlineGroceryPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineGroceryPortal.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly GroceryDbContext _context;
        public ProductRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetByIdAsync(long id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetByTypeAsync(string type)
        {
            return await _context.Products
                .Where(p => p.Type.ToLower() == type.ToLower())
                .ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}