namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IJsonGeneratorService
    {
        Task CreateDailyJsonFile(IStatisticsService statisticsService);
    }
}
