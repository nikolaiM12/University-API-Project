using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.DTOs;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IOrganizationService : IBaseService<OrganizationDto, int>
    {
        Task<List<OrganizationDataModel>> GetOrganizationDetails();
    }
}
