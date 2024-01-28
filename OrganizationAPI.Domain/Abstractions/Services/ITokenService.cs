using OrganizationAPI.Domain.Abstractions.DTOs.Account;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(AccountDto account);
    }
}
