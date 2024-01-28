namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class CreateDbAndTablesQueriesConstants
    {
        public const string CheckDatabaseExists = @"IF db_id('organization') IS NOT NULL SELECT 1 ELSE SELECT 0";

        public const string CreateDatabase = @"CREATE DATABASE organization";

        public const string UseOrganizationDatabase = "USE organization";

        public const string CheckTableExistsQuery = UseOrganizationDatabase + @"
            IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = @TableName)
                SELECT 1
            ELSE
                SELECT 0
        ";

        public const string CheckTableIndustriesExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'Industries')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CheckTableCountriesExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'Countries')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CheckTableOrganizationsExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'Organizations')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CheckTableAccountsExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'Accounts')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CheckTableUsersExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'Users')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CheckTableHistoricalDataExists = UseOrganizationDatabase + @"
        IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = 'HistoricalData')
            SELECT 1
        ELSE
            SELECT 0
        ";

        public const string CreateTableHistoricalData = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE HistoricalData
            (
                Id UNIQUEIDENTIFIER PRIMARY KEY,
                TableName NVARCHAR(MAX),
                DeletedAt DATETIME,
                DeletedBy NVARCHAR(255)
            );
        ";

        public const string CreateTableCountries = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE Countries (
                Id INT PRIMARY KEY IDENTITY,
                Name NVARCHAR(255) NOT NULL,
                IsDeleted BIT DEFAULT 0
            );
        ";

        public const string CreateTableOrganizations = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE Organizations (
                Id INT PRIMARY KEY,
                OrganizationId NVARCHAR(255) NOT NULL,
                Name NVARCHAR(255) NOT NULL,
                Website NVARCHAR(255) NOT NULL,
                CountryId INT NOT NULL,
                Description NVARCHAR(MAX),
                Founded INT NOT NULL,
                IndustryId INT NOT NULL,
                NumberOfEmployees INT NOT NULL,
                IsDeleted BIT DEFAULT 0,
                FOREIGN KEY (CountryId) REFERENCES Countries(Id),
                FOREIGN KEY (IndustryId) REFERENCES Industries(Id)
            )
        ";

        public const string CreateTableIndustries = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE Industries (
                Id INT PRIMARY KEY IDENTITY,
                Name NVARCHAR(255) NOT NULL,
                IsDeleted BIT DEFAULT 0
            );
        ";

        public const string CreateTableAccounts = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE Accounts (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                Email NVARCHAR(255) NOT NULL,
                PasswordHash NVARCHAR(255) NOT NULL,
                PasswordSalt NVARCHAR(255) NOT NULL,
                Role INT,
                IsDeleted BIT DEFAULT 0
            )
        ";

        public const string CreateTableUsers = @"
        " + UseOrganizationDatabase + @"
            CREATE TABLE Users (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                FirstName NVARCHAR(255) NOT NULL,
                LastName NVARCHAR(255) NOT NULL,
                Age INT NOT NULL,
                PhoneNumber NVARCHAR(20) NOT NULL,
                Country NVARCHAR(100) NOT NULL,
                IsDeleted BIT DEFAULT 0,
                AccountId UNIQUEIDENTIFIER DEFAULT NEWID(),
                FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
            )
        ";
    }
}
