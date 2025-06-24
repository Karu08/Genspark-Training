using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;


namespace OnlineGroceryPortal.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> PlaceOrderAsync(long customerId, OrderDto orderDto);
        Task<string?> GetOrderStatusAsync(long orderId);
        Task<bool> UpdateOrderStatusAsync(long orderId, string newStatus);


    }
}