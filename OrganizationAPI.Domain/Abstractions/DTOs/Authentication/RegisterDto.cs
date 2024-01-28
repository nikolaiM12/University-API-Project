using OrganizationAPI.Domain.Abstractions.Enums;

namespace OrganizationAPI.Domain.Abstractions.DTOs.Authentication
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
