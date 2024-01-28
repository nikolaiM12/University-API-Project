using OrganizationAPI.Domain.Abstractions.DTOs.Account;
namespace OrganizationAPI.Domain.Abstractions.Repositories
{
    public interface IAccountRepository : IRepository<AccountDto, Guid>
    {
        Task<AccountDto> GetAccountByEmail(string email);
    }
}
