
using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;

namespace OnlineGroceryPortal.Interfaces
{
    public interface IAuthenticationService
    {
        Task<TokenDto> RegisterAsync(RegisterDto dto);
        Task<TokenDto> LoginAsync(LoginDto dto);
        Task<TokenDto> RefreshTokenAsync(string RefreshToken);
    }
}