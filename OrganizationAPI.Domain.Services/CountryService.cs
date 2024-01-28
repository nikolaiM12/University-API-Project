using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class CountryService : BaseService<CountryDto, int>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository repository) : base(repository)
        {
            _countryRepository = repository;
        }

        public async Task<int> GetIdByCountryName(string countryName)
        {
            return await _countryRepository.GetIdByCountryName(countryName);
        }
    }
}
