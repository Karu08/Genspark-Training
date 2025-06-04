using System.Threading.Tasks;
using AutoMapper;
using FirstAPI.Interfaces;
using FirstAPI.Misc;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using FirstAPI.Contexts;

namespace FirstAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ClinicContext _context;

        public AppointmentService(ClinicContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelAppointment(int appointmentId, int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
                throw new ArgumentException("Doctor not found");

            if (doctor.YearsOfExperience < 3)
                throw new UnauthorizedAccessException("Doctor does not have enough experience to cancel appointments");

            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                throw new ArgumentException("Appointment not found");

            appointment.Status = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateAppointment(AppointmentCreateRequest request)
        {
            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            var patient = await _context.Patients.FindAsync(request.PatientId);

            if (doctor == null || patient == null)
                throw new ArgumentException("Invalid doctor or patient ID");

            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                ScheduledDate = request.ScheduledDate,
                Status = false // false = active, true = cancelled
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}