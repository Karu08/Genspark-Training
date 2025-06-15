using OnlineGroceryPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineGroceryPortal.Contexts
{
    public class GroceryDbContext : DbContext
    {
     
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
