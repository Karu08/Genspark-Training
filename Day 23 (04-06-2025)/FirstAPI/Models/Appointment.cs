using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAPI.Models
{
    public class Appointment
    {
    
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public bool Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}