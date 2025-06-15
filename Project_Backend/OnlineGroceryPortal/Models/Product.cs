namespace OnlineGroceryPortal.Models
{
    public class Product
    {
        public long Id { get; set; }

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
       

    }
}