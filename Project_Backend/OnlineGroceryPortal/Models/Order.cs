namespace OnlineGroceryPortal.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public int DeliveryAgentId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!; //cancelled, pending, ofd, delivered etc

        public ICollection<OrderItem>? Items { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}