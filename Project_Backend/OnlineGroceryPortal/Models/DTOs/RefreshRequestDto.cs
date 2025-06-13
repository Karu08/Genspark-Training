namespace OnlineGroceryPortal.Models.DTOs
{
    public class RefreshRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}