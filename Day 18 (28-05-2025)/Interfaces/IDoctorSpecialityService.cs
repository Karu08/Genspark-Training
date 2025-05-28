using FirstAPI.Models;

namespace FirstAPI.Interfaces
{
    public interface IDoctorSpecialityService
    {
        Task<IEnumerable<DoctorSpeciality>> GetAllDoctorSpecialities();
        Task<DoctorSpeciality?> GetDoctorSpecialityById(int doctorId, int specialityId);
        Task<DoctorSpeciality> AddDoctorSpeciality(DoctorSpeciality doctorSpeciality);
        Task<DoctorSpeciality> UpdateDoctorSpeciality(DoctorSpeciality doctorSpeciality);
        Task<DoctorSpeciality?> DeleteDoctorSpeciality(int doctorId, int specialityId);
    }
}
