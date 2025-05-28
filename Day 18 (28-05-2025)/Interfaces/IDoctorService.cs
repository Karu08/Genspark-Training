using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(int id);
        Task<Doctor> AddDoctor(Doctor doctor);
        
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(string specialization);
    }
}