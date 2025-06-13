
namespace OnlineGroceryPortal.Models.DTOs
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public string DeliveryAddress { get; set; } = null!;
        
    }
}