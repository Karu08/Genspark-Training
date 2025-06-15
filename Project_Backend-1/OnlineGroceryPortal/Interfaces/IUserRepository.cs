using OnlineGroceryPortal.Models;

namespace OnlineGroceryPortal.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
    }
}