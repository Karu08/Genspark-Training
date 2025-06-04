using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface IAppointmentService
    {
        //Task<IEnumerable<Appointment>> GetAllAppointments();
        //Task<Appointment?> GetAppointmentById(string id);
        //Task<Appointment> AddAppointment(Appointment appointment);
        //Task<Appointment> UpdateAppointment(Appointment appointment);
        //Task<Appointment?> DeleteAppointment(string id);
        Task<bool> CancelAppointment(int id, int doctorId);
        Task<bool> CreateAppointment(AppointmentCreateRequest request);

        //Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId);
        //Task<IEnumerable<Appointment>> GetAppointmentsByPatient(int patientId);
    }
}
