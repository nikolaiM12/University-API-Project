using OrganizationAPI.Domain.Abstractions.DTOs;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface ICountryService : IBaseService<CountryDto, int>
    {
        Task<int> GetIdByCountryName(string countryName);
    }
}
