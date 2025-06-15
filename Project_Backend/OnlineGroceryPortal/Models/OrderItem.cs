namespace OnlineGroceryPortal.Models
{
    public class OrderItem
    {
        public long Id { get; set; }

        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}