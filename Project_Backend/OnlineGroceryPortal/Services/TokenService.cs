using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Stores;

namespace OnlineGroceryPortal.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public TokenDto GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", user.Role)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            var refreshToken = Guid.NewGuid().ToString();

            RefreshTokenStore.Tokens[user.Username] = refreshToken;

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken,
                ExpiresAt = accessToken.ValidTo
            };
        }
    }
}