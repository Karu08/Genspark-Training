using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstAPI.Repositories
{
    public class SpecialityRepository : Repository<int, Speciality>
    {
        public SpecialityRepository(ClinicContext context) : base(context) { }

        public override async Task<Speciality> Get(int key)
        {
            return await _clinicContext.Specialities.FindAsync(key);
        }

        public override async Task<IEnumerable<Speciality>> GetAll()
        {
            return await _clinicContext.Specialities.ToListAsync();
        }
    }
}
