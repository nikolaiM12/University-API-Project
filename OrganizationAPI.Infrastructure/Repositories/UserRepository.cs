using AutoMapper;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.User;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Infrastructure.Data;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Repositories
{
    public class UserRepository : UserDataAccess, IUserRepository
    {
        public UserRepository(IDbConnector connection, IMapper mapper) : base(connection, mapper)
        {
        }
    }
}
