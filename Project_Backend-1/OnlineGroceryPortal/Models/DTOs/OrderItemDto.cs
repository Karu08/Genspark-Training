
namespace OnlineGroceryPortal.Models.DTOs
{
    public class OrderItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; } 
    }
}