namespace OnlineGroceryPortal.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Order>? Deliveries { get; set; }

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
    
}