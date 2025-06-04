
using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs.DoctorSpecialities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FirstAPI.Services;
using FirstAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FirstAPI.Misc;

namespace FirstAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ClinicContext _context;
        private readonly IAppointmentService _appointmentService;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            ILogger<AuthenticationController> logger,
            ClinicContext context,
            IAppointmentService appointmentService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _context = context;
            _appointmentService = appointmentService;
        }
        
        [HttpPost]
        [CustomExceptionFilter]
        public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest loginRequest)
        {
            /*try
            {
                var result = await _authenticationService.Login(loginRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Unauthorized(e.Message);
            }*/
            var result = await _authenticationService.Login(loginRequest);
            return Ok(result);
        }

        

    }
}