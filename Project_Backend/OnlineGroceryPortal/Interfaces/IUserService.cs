
using OnlineGroceryPortal.Models;

namespace OnlineGroceryPortal.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user);
    }
}