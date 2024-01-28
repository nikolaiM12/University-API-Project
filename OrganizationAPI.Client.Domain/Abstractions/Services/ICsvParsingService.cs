using OrganizationAPI.Client.Domain.Model;

namespace OrganizationAPI.Client.Domain.Abstractions.Services
{
    public interface ICsvParsingService
    {
        Task<List<OrganizationDataModel>> ParseCsvFile();
    }
}
