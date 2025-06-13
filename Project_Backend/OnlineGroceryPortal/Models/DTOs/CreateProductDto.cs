namespace OnlineGroceryPortal.Models.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}