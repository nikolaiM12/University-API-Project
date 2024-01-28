using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Repository;
using System.Data;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Data
{
    public abstract class OrganizationDataAccess : IRepository<OrganizationDto, int>
    {
        private readonly IDbConnector connection;
        private readonly ISectorRepository sectorRepository;
        private readonly ICountryRepository countryRepository;
        public OrganizationDataAccess(IDbConnector connection, ISectorRepository sectorRepository, ICountryRepository countryRepository)
        {
            this.connection = connection;
            this.sectorRepository = sectorRepository;
            this.countryRepository = countryRepository;
        }

        public async Task Add(List<OrganizationDto> organizations)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-DPN235VU\\SQLEXPRESS;Database=organization;Trusted_Connection=True;MultipleActiveResultSets=True"))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, transaction))
                            {
                                bulkCopy.BulkCopyTimeout = 0;
                                bulkCopy.DestinationTableName = "Organizations";

                                DataTable dataTable = new DataTable();
                                dataTable.Columns.Add("Id", typeof(int));
                                dataTable.Columns.Add("OrganizationId", typeof(string));
                                dataTable.Columns.Add("Name", typeof(string));
                                dataTable.Columns.Add("Website", typeof(string));
                                dataTable.Columns.Add("CountryId", typeof(int));
                                dataTable.Columns.Add("Description", typeof(string));
                                dataTable.Columns.Add("Founded", typeof(int));
                                dataTable.Columns.Add("IndustryId", typeof(int));
                                dataTable.Columns.Add("NumberOfEmployees", typeof(int));
                                dataTable.Columns.Add("IsDeleted", typeof(bool));

                                foreach (var organization in organizations)
                                {
                                    dataTable.Rows.Add(
                                        organization.Id,
                                        organization.OrganizationId,
                                        organization.Name,
                                        organization.Website,
                                        organization.CountryId,
                                        organization.Description,
                                        organization.Founded,
                                        organization.IndustryId,
                                        organization.NumberOfEmployees,
                                        organization.IsDeleted
                                    );
                                }

                                await bulkCopy.WriteToServerAsync(dataTable);

                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                    await sqlConnection.CloseAsync();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<OrganizationDto>> GetAll()
        {
            try
            {
                List<OrganizationDto> organizations = new List<OrganizationDto>();

                using (SqlCommand cmd = new SqlCommand(GetAllQueriesConstants.GetAllOrganizationsQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            OrganizationDto organization = new OrganizationDto();
                            organization.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            organization.OrganizationId = reader.GetString(reader.GetOrdinal("OrganizationId"));
                            organization.Name = reader.GetString(reader.GetOrdinal("Name"));
                            organization.Website = reader.GetString(reader.GetOrdinal("Website"));
                            organization.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                            organization.Description = reader.GetString(reader.GetOrdinal("Description"));
                            organization.Founded = reader.GetInt32(reader.GetOrdinal("Founded"));
                            organization.IndustryId = reader.GetInt32(reader.GetOrdinal("IndustryId"));
                            organization.NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"));
                            organization.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                            organizations.Add(organization);
                        }
                    }

                    await connection.CloseConnection();
                }

                return organizations;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<List<OrganizationDataModel>> GetOrganizationDetails()
        {
            try
            {
                List<OrganizationDataModel> organizations = new List<OrganizationDataModel>();

                using (SqlCommand cmd = new SqlCommand(InnerJoinQueriesConstants.OrganizationDetailsJoinQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            OrganizationDataModel organization = new OrganizationDataModel();
                            organization.Index = reader.GetInt32(reader.GetOrdinal("Id"));
                            organization.OrganizationId = reader.GetString(reader.GetOrdinal("OrganizationId"));
                            organization.Name = reader.GetString(reader.GetOrdinal("Name"));
                            organization.Website = reader.GetString(reader.GetOrdinal("Website"));
                            organization.Country = reader.GetString(reader.GetOrdinal("Name"));
                            organization.Description = reader.GetString(reader.GetOrdinal("Description"));
                            organization.Founded = reader.GetInt32(reader.GetOrdinal("Founded"));
                            organization.Industry = reader.GetString(reader.GetOrdinal("Name"));
                            organization.NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"));

                            organizations.Add(organization);
                        }
                    }

                    await connection.CloseConnection();
                }
                return organizations;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Update(OrganizationDto organization)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQueriesConstants.UpdateOrganization, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@Id", organization.Id);
                    cmd.Parameters.AddWithValue("@OrganizationId", organization.OrganizationId);
                    cmd.Parameters.AddWithValue("@Name", organization.Name);
                    cmd.Parameters.AddWithValue("@Website", organization.Website);
                    cmd.Parameters.AddWithValue("@CountryId", organization.CountryId);
                    cmd.Parameters.AddWithValue("@Description", organization.Description);
                    cmd.Parameters.AddWithValue("@Founded", organization.Founded);
                    cmd.Parameters.AddWithValue("@IndustryId", organization.IndustryId);
                    cmd.Parameters.AddWithValue("@NumberOfEmployees", organization.NumberOfEmployees);
                    cmd.Parameters.AddWithValue("@IsDeleted", organization.IsDeleted);

                    await cmd.ExecuteNonQueryAsync();

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.InnerException}");
            }
        }

        public async Task<OrganizationDto> GetById(int id)
        {
            try
            {
                OrganizationDto organization = new OrganizationDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetOrganizationById, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue("@OrganizationId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            organization = new OrganizationDto();
                            organization.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            organization.OrganizationId = reader.GetString(reader.GetOrdinal("OrganizationId"));
                            organization.Name = reader.GetString(reader.GetOrdinal("Name"));
                            organization.Website = reader.GetString(reader.GetOrdinal("Website"));
                            organization.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                            organization.Description = reader.GetString(reader.GetOrdinal("Description"));
                            organization.Founded = reader.GetInt32(reader.GetOrdinal("Founded"));
                            organization.IndustryId = reader.GetInt32(reader.GetOrdinal("IndustryId"));
                            organization.NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return organization;
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
                    using (SqlCommand cmd = new SqlCommand(SoftDeleteQueriesConstants.SoftDeleteOrganizationAndInsertIntoHistorical, connection.GetConnection()))
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
