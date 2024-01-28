using System.Data.SqlClient;

namespace OrganizationAPI.Domain.Abstractions.Database
{
    public abstract class DbTableCreatorBase
    {
        private readonly IDbConnector _connection;

        public DbTableCreatorBase(IDbConnector connection)
        {
            _connection = connection;
        }

        public async Task CreateTableIfNotExists(string checkQuery, string createQuery)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(checkQuery, _connection.GetConnection()))
                {
                    await _connection.OpenConnection();
                    bool tableExists = Convert.ToBoolean(await cmd.ExecuteScalarAsync());

                    if (!tableExists)
                    {
                        using (SqlCommand createCmd = new SqlCommand(createQuery, _connection.GetConnection()))
                        {
                            await createCmd.ExecuteNonQueryAsync();
                        }
                    }

                    await _connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public abstract Task CreateTables();
    }
}
