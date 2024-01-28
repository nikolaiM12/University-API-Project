using OrganizationAPI.Domain.Abstractions.DTOs.User;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IUserService : IBaseService<UserDto, Guid>
    {
    }
}
