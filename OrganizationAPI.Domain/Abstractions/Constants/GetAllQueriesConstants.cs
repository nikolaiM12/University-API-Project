namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class GetAllQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string GetAllAccountsQuery = UseOrganizationDatabase + @"
            SELECT * FROM Accounts
            WHERE IsDeleted = 0
        ";

        public const string GetAllUsersQuery = UseOrganizationDatabase + @"
            SELECT * FROM User
            WHERE IsDeleted = 0
        ";

        public const string GetAllCountriesQuery = UseOrganizationDatabase + @"
            SELECT * FROM Countries
            WHERE IsDeleted = 0
        ";

        public const string GetAllIndustriesQuery = UseOrganizationDatabase + @"
            SELECT * FROM Industries
            WHERE IsDeleted = 0
        ";

        public const string GetAllOrganizationsQuery = UseOrganizationDatabase + @"
            SELECT * FROM Organizations
            WHERE IsDeleted = 0
        ";
    }
}
