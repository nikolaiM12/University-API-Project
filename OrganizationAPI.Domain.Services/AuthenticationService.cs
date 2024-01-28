using AutoMapper;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.User;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Security.Principal;

namespace OrganizationAPI.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService accountService;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public AuthenticationService(IAccountService accountService, IPasswordService passwordService, ITokenService tokenService, IUserService userService, IMapper mapper)
        {
            this.accountService = accountService;
            this.passwordService = passwordService;
            this.tokenService = tokenService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<RegisterDto> Register(RegisterDto register)
        {
            var existingAccount = await accountService.GetAccountByEmail(register.Email);

            if (existingAccount != null)
            {
                UserDto userDto = mapper.Map<UserDto>(register);
                userDto.AccountId = existingAccount.Id;

                await userService.Add(new List<UserDto> { userDto });
                return register;
            }
            else
            {
                AccountDto accountDto = mapper.Map<AccountDto>(register);
                accountDto.Role = register.Role;

                await accountService.Add(new List<AccountDto> { accountDto });

                UserDto userDto = mapper.Map<UserDto>(register);
                userDto.Id = Guid.NewGuid();
                userDto.AccountId = accountDto.Id;

                await userService.Add(new List<UserDto> { userDto });

                return register;
            }
        }

        public async Task<string> Login(LoginDto login)
        {
            var account = await accountService.GetAccountByEmail(login.Email);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if (!passwordService.VerifyPassword(login.Password, account.PasswordSalt, account.PasswordHash))
            {
                throw new InvalidOperationException("Invalid password!");
            }

            string token = tokenService.GenerateJwtToken(account);

            return token;
        }
    }
}
