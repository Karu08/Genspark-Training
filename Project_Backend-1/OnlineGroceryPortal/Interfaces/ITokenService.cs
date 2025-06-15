
using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;

namespace OnlineGroceryPortal.Interfaces
{
    public interface ITokenService
    {
        TokenDto GenerateToken(User user);
    }
}