using CsvHelper;
using OrganizationAPI.Client.Domain.Abstractions.Configurations;
using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Infrastructure.Data;
using OrganizationAPI.Infrastructure.Database;
using OrganizationAPI.Infrastructure.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Xml.Linq;

namespace OrganizationAPI.Infrastructure.Repository
{
    public class OrganizationRepository : OrganizationDataAccess, IOrganizationRepository
    {
        public OrganizationRepository(IDbConnector connection, ISectorRepository sectorRepository, ICountryRepository countryRepository) : base(connection, sectorRepository, countryRepository)
        {
        }
    }
}
