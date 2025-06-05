using FileHandling.Models;
using FileHandling.Models.DTOs;

namespace FileHandling.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegistrationDto dto);
        Task<User?> ValidateUser(LoginDto dto);
    }
}