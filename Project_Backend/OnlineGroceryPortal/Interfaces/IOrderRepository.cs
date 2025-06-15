using OnlineGroceryPortal.Models;

namespace OnlineGroceryPortal.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order?> GetByIdAsync(long orderId);
        Task UpdateAsync(Order order);


    }
}