using DemoVersion.API.Abstractions.DTOs.Statistics;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.DTOs.Statistics;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Infrastructure.Database;
using System.Data.SqlClient;

namespace OrganizationAPI.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly DbConnector connection;
        public StatisticsService(DbConnector connection)
        {
            this.connection = connection;
        }
        public async Task<List<DescendingDto>> GetCompaniesByEmployeesDescending()
        {
            try
            {
                List<DescendingDto> companies = new List<DescendingDto>();

                using (SqlCommand cmd = new SqlCommand(StatisticsQueriesConstants.GetCompaniesByEmployeesDescending, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DescendingDto company = new DescendingDto();
                            company.Name = reader.GetString(reader.GetOrdinal("Name"));
                            company.NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"));

                            companies.Add(company);
                        }
                    }

                    await connection.CloseConnection();
                }

                return companies;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<TotalEmployeesByCountryAndIndustryDto>> GetTotalEmployeesByCountryAndIndustry()
        {
            try
            {
                List<TotalEmployeesByCountryAndIndustryDto> totalEmployeesByCountryAndIndustry = new List<TotalEmployeesByCountryAndIndustryDto>();

                using (SqlCommand cmd = new SqlCommand(StatisticsQueriesConstants.GetTotalEmployeesByCountryAndIndustry, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            TotalEmployeesByCountryAndIndustryDto totalEmployees = new TotalEmployeesByCountryAndIndustryDto();
                            totalEmployees.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                            totalEmployees.IndustryId = reader.GetInt32(reader.GetOrdinal("IndustryId"));
                            totalEmployees.TotalEmployees = reader.GetInt32(reader.GetOrdinal("TotalEmployees"));

                            totalEmployeesByCountryAndIndustry.Add(totalEmployees);
                        }
                    }

                    await connection.CloseConnection();
                }

                return totalEmployeesByCountryAndIndustry;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<TotalEmployeesByIndustryDto>> GetTotalEmployeesByIndustry()
        {
            try
            {
                List<TotalEmployeesByIndustryDto> totalEmployeesByIndustry = new List<TotalEmployeesByIndustryDto>();

                using (SqlCommand cmd = new SqlCommand(StatisticsQueriesConstants.GetTotalEmployeesByIndustry, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            TotalEmployeesByIndustryDto totalEmployees = new TotalEmployeesByIndustryDto();
                            totalEmployees.IndustryId = reader.GetInt32(reader.GetOrdinal("IndustryId"));
                            totalEmployees.TotalEmployees = reader.GetInt32(reader.GetOrdinal("TotalEmployees"));

                            totalEmployeesByIndustry.Add(totalEmployees);
                        }
                    }

                    await connection.CloseConnection();
                }

                return totalEmployeesByIndustry;

            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<CompaniesWithMostEmployeesDto>> GetCompaniesWithMostEmployees(int topN)
        {
            try
            {
                List<CompaniesWithMostEmployeesDto> companies = new List<CompaniesWithMostEmployeesDto>();

                using (SqlCommand cmd = new SqlCommand(StatisticsQueriesConstants.GetCompaniesWithMostEmployees, connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@TopN", topN);

                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CompaniesWithMostEmployeesDto company = new CompaniesWithMostEmployeesDto();
                            company.Name = reader.GetString(reader.GetOrdinal("Name"));
                            company.NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"));

                            companies.Add(company);
                        }
                    }

                    await connection.CloseConnection();
                }

                return companies;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<ComplexStatisticsDto>> GetComplexStatistics()
        {
            try
            {
                List<ComplexStatisticsDto> complexStatistics = new List<ComplexStatisticsDto>();

                using (SqlCommand cmd = new SqlCommand(StatisticsQueriesConstants.GetComplexStatistics, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ComplexStatisticsDto statistic = new ComplexStatisticsDto();

                            statistic.CountryName = reader.GetString(reader.GetOrdinal("CountryName"));
                            statistic.IndustryName = reader.GetString(reader.GetOrdinal("IndustryName"));
                            statistic.TotalEmployees = reader.GetInt32(reader.GetOrdinal("TotalEmployees"));

                            complexStatistics.Add(statistic);
                        }
                    }

                    await connection.CloseConnection();
                }

                return complexStatistics;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException?.Message ?? "N/A"}");
            }
        }
    }
}
