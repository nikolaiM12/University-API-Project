using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Text.Json;

namespace OrganizationAPI.Domain.Services
{
    public class JsonGeneratorService : IJsonGeneratorService
    {
        public async Task CreateDailyJsonFile(IStatisticsService statisticsService)
        {
            var descendingCompanies = await statisticsService.GetCompaniesByEmployeesDescending();
            var totalEmployeesByCountryAndIndustry = await statisticsService.GetTotalEmployeesByCountryAndIndustry();
            var totalEmployeesByIndustry = await statisticsService.GetTotalEmployeesByIndustry();
            var companiesWithMostEmployees = await statisticsService.GetCompaniesWithMostEmployees(10);
            var complexStatistics = await statisticsService.GetComplexStatistics();

            var statisticsData = new
            {
                DescendingCompanies = descendingCompanies,
                TotalEmployeesByCountryAndIndustry = totalEmployeesByCountryAndIndustry,
                TotalEmployeesByIndustry = totalEmployeesByIndustry,
                CompaniesWithMostEmployees = companiesWithMostEmployees,
                ComplexStatistics = complexStatistics
            };

            var jsonString = JsonSerializer.Serialize(statisticsData);

            var filePath = GenerateJsonFilePathConstants.JsonFilePath;

            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllTextAsync(filePath, jsonString);
        }
    }
}
