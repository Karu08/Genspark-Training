using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Stores; 

namespace OnlineGroceryPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public UserController(
            IAuthenticationService authService,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
            if (RefreshTokenStore.Tokens.TryGetValue(request.Username, out var savedToken))
            {
                if (savedToken == request.RefreshToken)
                {
                    var user = await _userRepository.GetByUsernameAsync(request.Username);
                    if (user == null)
                        return Unauthorized();

                    var newToken = _tokenService.GenerateToken(user);
                    return Ok(newToken);
                }
            }

            return Unauthorized(new { message = "Invalid refresh token" });
        }
    }
}
