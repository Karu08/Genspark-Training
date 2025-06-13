using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Repositories;
using OnlineGroceryPortal.Contexts;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Interfaces;
using Serilog;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly GroceryDbContext _context;

    public OrderService(IOrderRepository repo, GroceryDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<Order> PlaceOrderAsync(Guid customerId, OrderDto orderDto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            Status = "Pending",
            Items = orderDto.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };


        return await _repo.CreateAsync(order);
    }

    public async Task<string?> GetOrderStatusAsync(Guid orderId)
    {
        var order = await _repo.GetByIdAsync(orderId);
        return order?.Status;
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus)
    {
        Log.Information("Updating order {OrderId} to status {Status}", orderId, newStatus);
        var order = await _repo.GetByIdAsync(orderId);

        if (order == null)
            return false;

        order.Status = newStatus;
        order.UpdatedAt = DateTime.UtcNow;
        order.UpdatedBy = "DeliveryAgent"; 

        await _repo.UpdateAsync(order);
        return true;
    }

}
