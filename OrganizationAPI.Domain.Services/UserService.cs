using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.User;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationAPI.Domain.Services
{
    public class UserService : BaseService<UserDto, Guid>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
