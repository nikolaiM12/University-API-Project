using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class OrganizationService : BaseService<OrganizationDto, int>, IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository repository) : base(repository)
        {
            _organizationRepository = repository;
        }

        public async Task<List<OrganizationDataModel>> GetOrganizationDetails()
        {
            return await _organizationRepository.GetOrganizationDetails();
        }
    }
}
