namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class AddQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string InsertIntoCountries = UseOrganizationDatabase + @"
            IF NOT EXISTS (SELECT Id FROM Countries WHERE Name = @Name)
            BEGIN
                INSERT INTO Countries(Name) 
                VALUES (@Name)
            END
        ";

        public const string InsertIntoIndustries = UseOrganizationDatabase + @"
            IF NOT EXISTS (SELECT Id FROM Industries WHERE Name = @Name)
            BEGIN
                INSERT INTO Industries (Name)
                VALUES (@Name)
            END
        ";

        public const string InsertIntoOrganizations = UseOrganizationDatabase + @"
            IF NOT EXISTS (SELECT Id FROM Organizations WHERE OrganizationId = @OrganizationId)
            BEGIN
                INSERT INTO Organizations (Id, OrganizationId, Name, Website, CountryId, Description, Founded, IndustryId, NumberOfEmployees)
                VALUES (@Id, @OrganizationId, @Name, @Website, @CountryId, @Description, @Founded, @IndustryId, @NumberOfEmployees)
            END
        ";


        public const string InsertIntoAccount = UseOrganizationDatabase + @"
            IF NOT EXISTS (SELECT Id FROM Accounts WHERE Id = @Id)
            BEGIN
                INSERT INTO Accounts (Id, Email, PasswordHash, PasswordSalt, Role)
                VALUES (@Id, @Email, @PasswordHash, @PasswordSalt, @Role)
            END
        ";

        public const string InsertIntoUser = UseOrganizationDatabase + @"
            IF NOT EXISTS (SELECT Id FROM Users WHERE Id = @Id)
            BEGIN
                INSERT INTO Users (FirstName, LastName, Age, PhoneNumber, Country, AccountId)
                VALUES (@FirstName, @LastName, @Age, @PhoneNumber, @Country, @AccountId)
            END
        ";
    }
}
