using Microsoft.EntityFrameworkCore;
using FIRSTAPI.Models;

namespace FIRSTAPI.Contexts
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        // Add other DbSets like Doctors, Appointments here
    }
}
