using OrganizationAPI.Domain.Abstractions.Repository;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IIdCacheService
    {
        Task<int> GetIdByCountryName(string countryName, ICountryRepository countryRepository);
        Task<int> GetIdByIndustryName(string industryName, ISectorRepository sectorRepository);
    }
}
