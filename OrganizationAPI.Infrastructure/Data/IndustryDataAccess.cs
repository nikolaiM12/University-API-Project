using AutoMapper;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace OrganizationAPI.Infrastructure.Data
{
    public abstract class IndustryDataAccess : IRepository<IndustryDto, int>
    {
        private readonly IDbConnector connection;
        public IndustryDataAccess(IDbConnector connection)
        {
            this.connection = connection;
        }
        public async Task<List<IndustryDto>> GetAll()
        {
            try
            {
                List<IndustryDto> industries = new List<IndustryDto>();

                using (SqlCommand cmd = new SqlCommand(GetAllQueriesConstants.GetAllIndustriesQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            IndustryDto industry = new IndustryDto();
                            industry.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            industry.Name = reader.GetString(reader.GetOrdinal("Name"));
                            industry.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                            industries.Add(industry);
                        }
                    }

                    await connection.CloseConnection();
                }

                return industries;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Add(List<IndustryDto> industries)
        {
            foreach (var industry in industries)
            {
                try
                {
                    await connection.OpenConnection();

                    using (SqlCommand cmd = new SqlCommand(AddQueriesConstants.InsertIntoIndustries, connection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Id", industry.Id);
                        cmd.Parameters.AddWithValue("@Name", industry.Name);
                        cmd.Parameters.AddWithValue("@IsDeleted", industry.IsDeleted);

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

        public async Task<int> GetIdByIndustryName(string industryName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetIndustryIdByName, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@IndustryName", industryName);

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

        public async Task Update(IndustryDto industry)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQueriesConstants.UpdateIndustry, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@IndustryId", industry.Id);
                    cmd.Parameters.AddWithValue("@Name", industry.Name);
                    cmd.Parameters.AddWithValue("@IsDeleted", industry.IsDeleted);

                    await cmd.ExecuteNonQueryAsync();

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<IndustryDto> GetById(int id)
        {
            try
            {
                IndustryDto industry = new IndustryDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetIndustryById, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue(@"IndustryId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            industry = new IndustryDto();
                            industry.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            industry.Name = reader.GetString(reader.GetOrdinal("Name"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return industry;
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
                    using (SqlCommand cmd = new SqlCommand(SoftDeleteQueriesConstants.SoftDeleteIndustryAndInsertIntoHistorical, connection.GetConnection()))
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
