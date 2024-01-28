using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class IndustryService : BaseService<IndustryDto, int>, ISectorService
    {
        private readonly ISectorRepository _sectorRepository;
        public IndustryService(ISectorRepository repository) : base(repository)
        {
            _sectorRepository = repository;
        }

        public async Task<int> GetIdByIndustryName(string industryName)
        {
            return await _sectorRepository.GetIdByIndustryName(industryName);
        }
    }
}
