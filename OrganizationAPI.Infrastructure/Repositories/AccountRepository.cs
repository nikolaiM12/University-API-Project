using AutoMapper;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Infrastructure.Data;

namespace OrganizationAPI.Infrastructure.Repositories
{
    public class AccountRepository : AccountDataAccess, IAccountRepository
    {
        public AccountRepository(IDbConnector connection, IPasswordService passwordService, IMapper mapper) : base(connection, passwordService, mapper)
        {
        }
    }
}
