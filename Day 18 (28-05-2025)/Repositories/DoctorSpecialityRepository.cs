using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAPI.Repositories
{
    public class DoctorSpecialityRepository : Repository<int, DoctorSpeciality>
    {
        public DoctorSpecialityRepository(ClinicContext context) : base(context) { }

        public override async Task<DoctorSpeciality> Get(int key)
        {
            return await _clinicContext.DoctorSpecialities.FindAsync(key);
        }

        public override async Task<IEnumerable<DoctorSpeciality>> GetAll()
        {
            return await _clinicContext.DoctorSpecialities.ToListAsync();
        }
    }
}
