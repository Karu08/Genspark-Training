using OnlineGroceryPortal.Contexts;
using OnlineGroceryPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineGroceryPortal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GroceryDbContext _context;
        public OrderRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync(long orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


    }
}