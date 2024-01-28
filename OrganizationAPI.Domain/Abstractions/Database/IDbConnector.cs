using System.Data.SqlClient;

namespace OrganizationAPI.Domain.Abstractions.Database
{
    public interface IDbConnector
    {
        Task OpenConnection();
        Task CloseConnection();
        SqlConnection GetConnection();
    }
}
