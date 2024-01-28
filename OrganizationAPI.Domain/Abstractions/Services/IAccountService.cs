using OrganizationAPI.Domain.Abstractions.DTOs.Account;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IAccountService : IBaseService<AccountDto, Guid>
    {
        Task<AccountDto> GetAccountByEmail(string email);
    }
}
