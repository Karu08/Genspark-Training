// Models/DTOs/Appointments/AppointmentCreateRequest.cs
namespace FirstAPI.Models.DTOs
{
    public class AppointmentCreateRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
