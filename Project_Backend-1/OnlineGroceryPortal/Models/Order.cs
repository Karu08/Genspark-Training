namespace OnlineGroceryPortal.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int DeliveryAgentId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!; //cancelled, pending, ofd, delivered etc

        public ICollection<OrderItem>? Items { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
      
    }
}