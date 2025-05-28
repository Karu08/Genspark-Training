using FirstAPI.Interfaces;
using FirstAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DoctorService : IDoctorService
{
    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Speciality> _specialityRepository;
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;

    public DoctorService(
        IRepository<int, Doctor> doctorRepository,
        IRepository<int, Speciality> specialityRepository,
        IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
        _doctorSpecialityRepository = doctorSpecialityRepository;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _doctorRepository.GetAll();
    }

    public async Task<Doctor?> GetDoctorById(int id)
    {
        return await _doctorRepository.Get(id);
    }

    public async Task<Doctor> AddDoctor(Doctor doctor)
    {
        return await _doctorRepository.Add(doctor);
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(string specialization)
    {
        var specialities = await _specialityRepository.GetAll();
        var matchedSpeciality = specialities
            .FirstOrDefault(s => s.Name.Equals(specialization, System.StringComparison.OrdinalIgnoreCase));

        if (matchedSpeciality == null)
        {
            return new List<Doctor>();
        }

        var doctorSpecialities = await _doctorSpecialityRepository.GetAll();
        var matchedDoctorIds = doctorSpecialities
            .Where(ds => ds.SpecialityId == matchedSpeciality.Id)
            .Select(ds => ds.DoctorId)
            .Distinct()
            .ToList();

        var allDoctors = await _doctorRepository.GetAll();
        return allDoctors.Where(d => matchedDoctorIds.Contains(d.Id));
    }
}
