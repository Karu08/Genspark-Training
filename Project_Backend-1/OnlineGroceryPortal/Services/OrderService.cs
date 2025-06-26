using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Repositories;
using OnlineGroceryPortal.Contexts;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Interfaces;
using Serilog;

namespace OnlineGroceryPortal.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly GroceryDbContext _context;

        public OrderService(IOrderRepository repo, GroceryDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Order?> PlaceOrderAsync(long customerId, OrderDto orderDto)
{
    
    var productMap = new Dictionary<long, Product>();

    foreach (var item in orderDto.Items)
    {
        var product = await _context.Products.FindAsync(item.ProductId);

        if (product == null || product.IsDeleted)
        {
            Log.Warning("Product {ProductId} not found or has been deleted", item.ProductId);
            return null;
        }

        if (item.Quantity > product.StockQty)
        {
            Log.Warning("Insufficient stock for product {ProductId}. Requested: {Qty}, Available: {Stock}",
                        item.ProductId, item.Quantity, product.StockQty);
            return null;
        }

        productMap[item.ProductId] = product; 
    }

    
    var orderItems = new List<OrderItem>();

    foreach (var item in orderDto.Items)
    {
        var product = productMap[item.ProductId];

        orderItems.Add(new OrderItem
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Price = product.Price * item.Quantity,
            CreatedAt = DateTime.UtcNow
        });
    }

    
    var order = new Order
    {
        CustomerId = customerId,
        DeliveryAgentId = 3,
        //DeliveryAddress = orderDto.DeliveryAddress,
        OrderDate = DateTime.UtcNow,
        //CreatedAt = DateTime.UtcNow,
        Status = "Pending",
        Items = orderItems
    };

    var savedOrder = await _repo.CreateAsync(order);

    
    foreach (var item in savedOrder.Items)
    {
        var product = productMap[item.ProductId];

        product.StockQty -= item.Quantity;
        _context.Products.Update(product);
    }

    await _context.SaveChangesAsync();
    return savedOrder;
}



           
        public async Task<string?> GetOrderStatusAsync(long orderId)
        {
            var order = await _repo.GetByIdAsync(orderId);
            return order?.Status;
        }

        public async Task<bool> UpdateOrderStatusAsync(long orderId, string newStatus)
        {
            Log.Information("Updating order {OrderId} to status {Status}", orderId, newStatus);
            var order = await _repo.GetByIdAsync(orderId);

            if (order == null)
                return false;

            order.Status = newStatus;
         

            await _repo.UpdateAsync(order);
            return true;
        }
    }
}
