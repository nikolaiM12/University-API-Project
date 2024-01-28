using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repositories;

namespace OrganizationAPI.Domain.Abstractions.Repository
{
    public interface ISectorRepository : IRepository<IndustryDto, int>
    {
        // specific operations
        Task<int> GetIdByIndustryName(string countryName);
    }
}
