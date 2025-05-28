using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface ISpecialityService
    {
        Task<IEnumerable<Speciality>> GetAllSpecialities();
        Task<Speciality?> GetSpecialityById(int id);
        Task<Speciality> AddSpeciality(Speciality speciality);
        Task<Speciality> UpdateSpeciality(Speciality speciality);
        Task<Speciality?> DeleteSpeciality(int id);
    }
}
