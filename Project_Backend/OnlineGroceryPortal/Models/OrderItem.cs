namespace OnlineGroceryPortal.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}