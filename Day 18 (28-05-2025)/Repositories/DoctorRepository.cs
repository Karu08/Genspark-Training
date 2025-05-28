using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAPI.Repositories
{
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository(ClinicContext context) : base(context) { }

        public override async Task<Doctor> Get(int key)
        {
            return await _clinicContext.Doctors.FindAsync(key);
        }

        public override async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _clinicContext.Doctors.ToListAsync();
        }
    }
}
