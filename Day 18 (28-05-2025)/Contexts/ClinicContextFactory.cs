using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FirstAPI.Contexts;

namespace FirstAPI.Contexts
{
    public class ClinicContextFactory : IDesignTimeDbContextFactory<ClinicContext>
    {
        public ClinicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=myDB;Username=postgres;Password=snowbells");

            return new ClinicContext(optionsBuilder.Options);
        }
    }
}
