using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class SoftDeleteQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string SoftDeleteAccountAndInsertIntoHistorical = UseOrganizationDatabase + @"
            DECLARE @AccountId UNIQUEIDENTIFIER;
            DECLARE @DeletedAt DATETIME;
            DECLARE @DeletedByValue NVARCHAR(255);
            DECLARE @Name NVARCHAR(MAX);
    
            SELECT @AccountId = Id, @DeletedAt = GETDATE(), @DeletedByValue = @DeletedByParam, @Name = 'Accounts'
            FROM Accounts
            WHERE Id = @Id;

            UPDATE Accounts
            SET IsDeleted = 1
            WHERE Id = @AccountId;

            INSERT INTO HistoricalData (Id, TableName, DeletedAt, DeletedBy)
            VALUES (@AccountId, @Name, @DeletedAt, @DeletedByValue);
        ";

        public const string SoftDeleteCountryAndInsertIntoHistorical = UseOrganizationDatabase + @"
            DECLARE @CountryId INT;
            DECLARE @DeletedAt DATETIME;
            DECLARE @DeletedByValue NVARCHAR(255);
            DECLARE @Name NVARCHAR(MAX);

            SET @CountryId = @Id;

            SELECT @DeletedAt = GETDATE(), @DeletedByValue = @DeletedByParam, @Name = 'Countries'
            FROM Countries
            WHERE Id = @CountryId;

            UPDATE Countries
            SET IsDeleted = 1
            WHERE Id = @CountryId;

            DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

            INSERT INTO HistoricalData (Id, TableName, DeletedAt, DeletedBy)
            VALUES (@NewId, @Name, @DeletedAt, @DeletedByValue);
        ";

        public const string SoftDeleteIndustryAndInsertIntoHistorical = UseOrganizationDatabase + @"
            DECLARE @IndustryId INT;
            DECLARE @DeletedAt DATETIME;
            DECLARE @DeletedByValue NVARCHAR(255);
            DECLARE @Name NVARCHAR(MAX);

            SET @IndustryId = @Id;

            SELECT @DeletedAt = GETDATE(), @DeletedByValue = @DeletedByParam, @Name = 'Industries'
            FROM Industries
            WHERE Id = @IndustryId;

            UPDATE Industries
            SET IsDeleted = 1
            WHERE Id = @IndustryId;

            DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

            INSERT INTO HistoricalData (Id, TableName, DeletedAt, DeletedBy)
            VALUES (@NewId, @Name, @DeletedAt, @DeletedByValue);
        ";

        public const string SoftDeleteOrganizationAndInsertIntoHistorical = UseOrganizationDatabase + @"
            DECLARE @OrganizationId INT;
            DECLARE @DeletedAt DATETIME;
            DECLARE @DeletedByValue NVARCHAR(255);
            DECLARE @Name NVARCHAR(MAX);

            SET @OrganizationId = @Id;

            SELECT @DeletedAt = GETDATE(), @DeletedByValue = @DeletedByParam, @Name = 'Organizations'
            FROM Organizations
            WHERE Id = @OrganizationId;

            UPDATE Organizations
            SET IsDeleted = 1
            WHERE Id = @OrganizationId;

            DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

            INSERT INTO HistoricalData (Id, TableName, DeletedAt, DeletedBy)
            VALUES (@NewId, @Name, @DeletedAt, @DeletedByValue);
        ";

        public const string SoftDeleteUserAndInsertIntoHistorical = UseOrganizationDatabase + @"
            DECLARE @UserId UNIQUEIDENTIFIER;
            DECLARE @DeletedAt DATETIME;
            DECLARE @DeletedByValue NVARCHAR(255);
            DECLARE @Name NVARCHAR(MAX);
    
            SELECT @UserId = Id, @DeletedAt = GETDATE(), @DeletedByValue = @DeletedByParam, @Name = 'Users'
            FROM Users
            WHERE Id = @Id;

            UPDATE Users
            SET IsDeleted = 1
            WHERE Id = @UserId;

            INSERT INTO HistoricalData (Id, TableName, DeletedAt, DeletedBy)
            VALUES (@UserId, @Name, @DeletedAt, @DeletedByValue);
        ";
    }
}
