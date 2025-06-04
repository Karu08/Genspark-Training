using Microsoft.AspNetCore.Authorization;

namespace FirstAPI.PolicyBasedAuthorization
{

    public class MinimumExperienceRequirement : IAuthorizationRequirement
    {
        public int MinimumYears { get; }

        public MinimumExperienceRequirement(int minimumYears)
        {
            MinimumYears = minimumYears;
        }
    }
}
