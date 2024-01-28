using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterDto> Register(RegisterDto register);
        Task<string> Login(LoginDto login);
    }
}
