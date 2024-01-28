using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;

namespace OrganizationAPI.Infrastructure.Database
{
    public class DbTableCreator : DbTableCreatorBase
    {
        public DbTableCreator(IDbConnector connection) : base(connection)
        {
        }

        public override async Task CreateTables()
        {
            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableCountriesExists,
                CreateDbAndTablesQueriesConstants.CreateTableCountries
            );

            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableIndustriesExists,
                CreateDbAndTablesQueriesConstants.CreateTableIndustries
            );

            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableOrganizationsExists,
                CreateDbAndTablesQueriesConstants.CreateTableOrganizations
            );

            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableAccountsExists,
                CreateDbAndTablesQueriesConstants.CreateTableAccounts
            );

            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableUsersExists,
                CreateDbAndTablesQueriesConstants.CreateTableUsers
            );

            await CreateTableIfNotExists(
                CreateDbAndTablesQueriesConstants.CheckTableHistoricalDataExists,
                CreateDbAndTablesQueriesConstants.CreateTableHistoricalData
            );
        }
    }
}
