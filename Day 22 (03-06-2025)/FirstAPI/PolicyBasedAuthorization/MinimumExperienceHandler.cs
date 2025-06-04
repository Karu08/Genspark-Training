using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FirstAPI.Contexts;
using FirstAPI.PolicyBasedAuthorization;


namespace FirstAPI.PolicyBasedAuthorization
{
    public class MinimumExperienceHandler : AuthorizationHandler<MinimumExperienceRequirement>
    {
        private readonly ClinicContext _clinicContext;

        public MinimumExperienceHandler(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        protected override async Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                MinimumExperienceRequirement requirement)
        {
            var username = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username))
                return;

            
            var doctor = await _clinicContext.Doctors
                    .FirstOrDefaultAsync(d => d.Email == username);

            if (doctor == null)
                return;

            if (doctor.YearsOfExperience >= requirement.MinimumYears)
            {
                context.Succeed(requirement);
            }
        }

    }
}
