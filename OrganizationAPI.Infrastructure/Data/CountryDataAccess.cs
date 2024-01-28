using Microsoft.SqlServer.Management.Smo;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace OrganizationAPI.Infrastructure.Data
{
    public abstract class CountryDataAccess : IRepository<CountryDto, int>
    {
        private readonly IDbConnector connection;
        public CountryDataAccess(IDbConnector connection)
        {
            this.connection = connection;
        }
        public async Task<List<CountryDto>> GetAll()
        {
            try
            {
                List<CountryDto> countries = new List<CountryDto>();

                using (SqlCommand cmd = new SqlCommand(GetAllQueriesConstants.GetAllCountriesQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CountryDto country = new CountryDto();
                            country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            country.Name = reader.GetString(reader.GetOrdinal("Name"));
                            country.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                            countries.Add(country);
                        }
                    }

                    await connection.CloseConnection();
                }

                return countries;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Add(List<CountryDto> countries)
        {
            foreach (var country in countries)
            {
                try
                {
                    await connection.OpenConnection();

                    using (SqlCommand cmd = new SqlCommand(AddQueriesConstants.InsertIntoCountries, connection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Id", country.Id);
                        cmd.Parameters.AddWithValue("@Name", country.Name);
                        cmd.Parameters.AddWithValue("@IsDeleted", country.IsDeleted);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    await connection.CloseConnection();
                }
                catch (SqlException e)
                {
                    throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
                }
            }
        }

        public async Task<int> GetIdByCountryName(string countryName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetCountryIdByName, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue("@CountryName", countryName);

                    var result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException}");
            }

            return -1;
        }

        public async Task Update(CountryDto country)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQueriesConstants.UpdateCountry, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@CountryId", country.Id);
                    cmd.Parameters.AddWithValue("@Name", country.Name);
                    cmd.Parameters.AddWithValue("@IsDeleted", country.IsDeleted);

                    await cmd.ExecuteNonQueryAsync();

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<CountryDto> GetById(int id)
        {
            try
            {
                CountryDto country = new CountryDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetCountryById, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue(@"CountryId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            country = new CountryDto();
                            country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            country.Name = reader.GetString(reader.GetOrdinal("Name"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return country;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Delete(int id)
        {
            Role role = new Role();
            if (role == Role.Admin)
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(SoftDeleteQueriesConstants.SoftDeleteCountryAndInsertIntoHistorical, connection.GetConnection()))
                    {
                        await connection.OpenConnection();

                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@DeletedByParam", $"{role}");

                        await cmd.ExecuteNonQueryAsync();

                        await connection.CloseConnection();
                    }
                }
                catch (SqlException e)
                {
                    throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin users can perform soft delete operations.");
            }
        }
    }
}
