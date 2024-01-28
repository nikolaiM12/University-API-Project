using OrganizationAPI.Domain.Abstractions.DTOs;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface ISectorService : IBaseService<IndustryDto, int>
    {
        Task<int> GetIdByIndustryName(string industryName);
    }
}
