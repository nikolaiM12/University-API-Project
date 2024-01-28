using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Infrastructure.Data;
using OrganizationAPI.Infrastructure.Database;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace OrganizationAPI.Infrastructure.Repositories
{
    public class IndustryRepository : IndustryDataAccess, ISectorRepository
    {
        public IndustryRepository(IDbConnector connection) : base(connection)
        {
        }
    }
}
