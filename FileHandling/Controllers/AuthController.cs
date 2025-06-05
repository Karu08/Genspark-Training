using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FileHandling.Contexts;
using FileHandling.Interfaces;
using FileHandling.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;


    public AuthController(AppDbContext context, ITokenService tokenService, IUserService userService)
    {
        _context = context;
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u =>
            u.Username == login.Username && u.Password == login.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register(RegistrationDto dto)
    {
        var result = await _userService.RegisterUser(dto);

        if (!result)
            return BadRequest("Username already exists.");

        return Ok("User registered successfully.");
    }
}

