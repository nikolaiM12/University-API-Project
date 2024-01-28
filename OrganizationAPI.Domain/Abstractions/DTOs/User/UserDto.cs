using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.Base;

namespace OrganizationAPI.Domain.Abstractions.DTOs.User
{
    public class UserDto : BaseDto
    {
        public override Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public Guid AccountId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
