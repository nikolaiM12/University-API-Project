using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class AccountService : BaseService<AccountDto, Guid>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository repository) : base(repository)
        {
            _accountRepository = repository;
        }

        public async Task<AccountDto> GetAccountByEmail(string email)
        {
            return await _accountRepository.GetAccountByEmail(email);
        }
    }
}
