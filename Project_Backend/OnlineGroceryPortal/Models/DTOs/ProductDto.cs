namespace OnlineGroceryPortal.Models.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = string.Empty;
        public float Price { get; set; }
        public float Stock { get; set; }
    }
}
