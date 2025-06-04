using FirstAPI.Interfaces;
using FirstAPI.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ClinicContext _context;

        public AppointmentController(IAppointmentService appointmentService, ClinicContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }



        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateRequest request)
        {
            try
            {
                var result = await _appointmentService.CreateAppointment(request);
                return result ? Ok("Appointment created") : BadRequest("Creation failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Policy = "ExperiencedDoctorOnly")]
        [HttpDelete("cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Doctor email not found in token");

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email);
            if (doctor == null)
                return NotFound("Doctor not found");

            await _appointmentService.CancelAppointment(id, doctor.Id);
            return Ok("Appointment cancelled");
        }
    }
}
