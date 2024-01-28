using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Database
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IDbConnector connection;
        public DbInitializer(IDbConnector connection)
        {
            this.connection = connection;
        }

        public async Task CreateDatabase()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(CreateDbAndTablesQueriesConstants.CheckDatabaseExists, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    bool databaseExists = Convert.ToBoolean(await cmd.ExecuteScalarAsync());

                    if (!databaseExists)
                    {
                        cmd.CommandText = CreateDbAndTablesQueriesConstants.CreateDatabase;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }
    }
}
