using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByNameAsync(string name);
        Task<ICollection<Doctor>> GetBySpecialityAsync(string speciality);
        Task<Doctor?> GetByNameAndContactAsync(string name, string contactNumber);
        Task<Doctor> AddAsync(Doctor doctor);
       
    }
}