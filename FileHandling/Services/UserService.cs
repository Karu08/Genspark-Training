using FileHandling.Contexts;
using FileHandling.Models;
using FileHandling.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using FileHandling.Interfaces;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterUser(RegistrationDto dto)
    {
        // Check if username already exists
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return false;

        var user = new User
        {
            Name = dto.Name,
            Username = dto.Username,
            Password = dto.Password, 
            Role = dto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> ValidateUser(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null) return null;

        if (user.Password != dto.Password)
            return null;

        return user;
    }
}
