using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.Base;
using OrganizationAPI.Domain.Abstractions.Enums;

namespace OrganizationAPI.Domain.Abstractions.DTOs.Account
{
    public class AccountDto : BaseDto
    {
        public override Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
