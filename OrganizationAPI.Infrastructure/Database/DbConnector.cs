using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using System.Data;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Database
{
    public class DbConnector : IDbConnector
    {
        private readonly SqlConnection connection;
        public DbConnector()
        {
            connection = new SqlConnection(DatabaseConnectionConstants.ConnectionString);
        }

        public SqlConnection GetConnection()
        { 
            return connection;
        }

        public async Task OpenConnection()
        {
            try
            {
                if(connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
            }
            catch(SqlException ex)
            {
                throw new InvalidOperationException($"Error: {ex.Message}. SQL Server error: {ex.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task CloseConnection()
        {
            try
            {
                if(connection.State != ConnectionState.Closed)
                {
                    await connection.CloseAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error: {ex.Message}. SQL Server error: {ex.InnerException?.Message ?? "N/A"}");
            }
        }
    }
}
