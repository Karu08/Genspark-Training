using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;


namespace OnlineGroceryPortal.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(Guid customerId, OrderDto orderDto);
        Task<string?> GetOrderStatusAsync(Guid orderId);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus);


    }
}