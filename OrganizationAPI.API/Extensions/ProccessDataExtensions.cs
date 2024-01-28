using OrganizationAPI.Client.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OrganizationAPI.API.Extensions
{
    public static class ProccessDataExtensions
    {
        public static async Task<long> ProcessData(IServiceProvider serviceProvider)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var parseCsvFile = scope.ServiceProvider.GetRequiredService<ICsvParsingService>();
                    var countryService = scope.ServiceProvider.GetRequiredService<ICountryRepository>();
                    var industryService = scope.ServiceProvider.GetRequiredService<ISectorRepository>();
                    var organizationService = scope.ServiceProvider.GetRequiredService<IOrganizationService>();
                    var idCacheService = scope.ServiceProvider.GetRequiredService<IIdCacheService>();

                    var records = await parseCsvFile.ParseCsvFile();

                    var countryInsertStopwatch = Stopwatch.StartNew();

                    var uniqueCountries = records
                        .GroupBy(data => data.Country)
                        .Select(group => group.First())
                        .Select((country, index) => new CountryDto
                        {
                            Id = index + 1,
                            Name = country.Country,
                            IsDeleted = false
                        })
                        .ToList();

                    await Task.WhenAll(
                        countryService.Add(uniqueCountries)
                    );

                    var uniqueIndustries = records
                        .GroupBy(data => data.Industry)
                        .Select(group => group.First())
                        .Select((industry, index) => new IndustryDto
                        {
                            Id = index + 1,
                            Name = industry.Industry,
                            IsDeleted = false
                        })
                        .ToList();

                    await Task.WhenAll(
                        industryService.Add(uniqueIndustries)
                    );

                    var organizationTasks = records.Select(async data =>
                    {
                        var countryId = await idCacheService.GetIdByCountryName(data.Country, countryService);
                        var industryId = await idCacheService.GetIdByIndustryName(data.Industry, industryService);

                        return new OrganizationDto
                        {
                            Id = data.Index,
                            OrganizationId = data.OrganizationId,
                            Name = data.Name,
                            Website = data.Website,
                            CountryId = countryId,
                            Description = data.Description,
                            Founded = data.Founded,
                            IndustryId = industryId,
                            NumberOfEmployees = data.NumberOfEmployees
                        };
                    }).ToList();

                    var organizationDtos = await Task.WhenAll(organizationTasks);

                    await organizationService.Add(organizationDtos.ToList());

                    countryInsertStopwatch.Stop();
                    var countryElapsedSeconds = stopwatch.ElapsedMilliseconds / 1000;
                    Console.WriteLine($"Total elapsed time for inserting countries, industries and organizations: {countryElapsedSeconds} seconds");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                foreach (SqlError error in sqlEx.Errors)
                {
                    Console.WriteLine($"SQL Error: {error.Message}");
                }
            }

            stopwatch.Stop();
            var elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000;

            Console.WriteLine($"Total elapsed time: {elapsedSeconds} seconds");
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
