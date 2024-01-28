using OrganizationAPI.Domain.Abstractions.DTOs.User;

namespace OrganizationAPI.Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<UserDto, Guid>
    {
    }
}
