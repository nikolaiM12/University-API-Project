using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repositories;

namespace OrganizationAPI.Domain.Abstractions.Repository
{
    public interface IOrganizationRepository : IRepository<OrganizationDto, int>
    {
        // specific operations
        // companies by employees desc
        // total employeess by industry
        // total employeess by industry & country
        // get data likely the csv file parsing
        Task<List<OrganizationDataModel>> GetOrganizationDetails();
    }
}
