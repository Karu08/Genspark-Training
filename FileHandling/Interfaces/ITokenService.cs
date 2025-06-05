using FileHandling.Models;

namespace FileHandling.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}