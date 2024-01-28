using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repositories;

namespace OrganizationAPI.Domain.Abstractions.Repository
{
    public interface ICountryRepository : IRepository<CountryDto, int>
    {
        // specific operations
        // order by operations, maybe group by with inner joins 
        // get country id by the name
        Task<int> GetIdByCountryName(string countryName);
    }
}
