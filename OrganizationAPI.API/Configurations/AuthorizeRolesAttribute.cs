using Microsoft.AspNetCore.Authorization;
using OrganizationAPI.Domain.Abstractions.Enums;

namespace OrganizationAPI.API.Configurations
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] allowedRoles) 
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(Role), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
