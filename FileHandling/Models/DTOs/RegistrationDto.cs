namespace FileHandling.Models.DTOs
{


    public class RegistrationDto
    {
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = string.Empty;
    }
}