namespace OnlineGroceryPortal.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ProdName { get; set; } = null!;
        public string Type { get; set; } = null!;  //fruit, veg, etc
        public string Description { get; set; } = string.Empty;
        public float StockQty { get; set; }

        public float Price { get; set; }
        public string ImageURL { get; set; } = string.Empty;

        //soft delete
        public bool IsDeleted { get; set; } = false;

        //audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

    }
}