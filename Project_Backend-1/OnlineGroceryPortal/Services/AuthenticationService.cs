
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Services;
using OnlineGroceryPortal.Models;

namespace OnlineGroceryPortal.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        private readonly IPasswordHasher _pwdHasher;

        public AuthenticationService(IUserRepository userRepo, ITokenService tokenService, IPasswordHasher pwdHasher)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _pwdHasher = pwdHasher;
        }

        public async Task<TokenDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepo.GetByUsernameAsync(dto.Username) != null)
                throw new Exception("Username already exists!");

            var hashedPassword = _pwdHasher.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = dto.Role,
                Email = dto.Email,
                PhoneNum = dto.PhoneNum
            };

            await _userRepo.CreateAsync(user);
            return _tokenService.GenerateToken(user);
        }

        public async Task<TokenDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            if (user == null || !_pwdHasher.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _tokenService.GenerateToken(user);
        }

        public async Task<TokenDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepo.GetByRefreshTokenAsync(refreshToken);
            if (user == null)
                throw new Exception("Invalid Refresh Token");

            return _tokenService.GenerateToken(user);
        }
    }
}