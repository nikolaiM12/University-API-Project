using DemoVersion.API.Abstractions.DTOs.Statistics;
using OrganizationAPI.Domain.Abstractions.DTOs.Statistics;

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IStatisticsService
    {
        Task<List<DescendingDto>> GetCompaniesByEmployeesDescending();
        Task<List<TotalEmployeesByIndustryDto>> GetTotalEmployeesByIndustry();
        Task<List<TotalEmployeesByCountryAndIndustryDto>> GetTotalEmployeesByCountryAndIndustry();
        Task<List<CompaniesWithMostEmployeesDto>> GetCompaniesWithMostEmployees(int topN);
        Task<List<ComplexStatisticsDto>> GetComplexStatistics();
    }
}
