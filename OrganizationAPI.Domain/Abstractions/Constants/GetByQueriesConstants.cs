namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class GetByQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string GetUserById = UseOrganizationDatabase + @"
            SELECT FirstName, LastName, Age, PhoneNumber, Country, AccountId 
            FROM Users
            WHERE Id = @UserId
        ";

        public const string GetOrganizationById = UseOrganizationDatabase + @"
            SELECT Id, OrganizationId, Name, Website, CountryId, Description, Founded, IndustryId, NumberOfEmployees 
            FROM Organizations 
            WHERE Id = @OrganizationId
        ";

        public const string GetAccountById = UseOrganizationDatabase + @"
            SELECT Id, Email, PasswordHash, PasswordSalt, Role 
            FROM Accounts    
            WHERE Id = @AccountId
        ";

        public const string GetCountryById = UseOrganizationDatabase + @"
            SELECT Id, Name 
            FROM Countries
            WHERE Id = @CountryId
        ";

        public const string GetIndustryById = UseOrganizationDatabase + @"
            SELECT Id, Name 
            FROM Industries 
            WHERE Id = @IndustryId
        ";

        public const string GetCountryIdByName = UseOrganizationDatabase + @"
            SELECT Id 
            FROM Countries 
            WHERE Name = @CountryName
        ";

        public const string GetIndustryIdByName = UseOrganizationDatabase + @"
            SELECT Id 
            FROM Industries 
            WHERE Name = @IndustryName
        ";

        public const string GetAccountsByEmail = UseOrganizationDatabase + @"
            SELECT Id, Email, PasswordHash, PasswordSalt, Role 
            FROM Accounts
            WHERE Email = @Email
        ";
    }
}
