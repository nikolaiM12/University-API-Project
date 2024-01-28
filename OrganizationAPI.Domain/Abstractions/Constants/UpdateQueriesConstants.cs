namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class UpdateQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string UpdateAccount = UseOrganizationDatabase + @"
            UPDATE Accounts
            SET Email = @Email,
                PasswordHash = @PasswordHash,
                PasswordSalt = @PasswordSalt,
                Role = @Role,
                IsDeleted = @IsDeleted
            WHERE Id = @AccountId
        ";

        public const string UpdateCountry = UseOrganizationDatabase + @"
            UPDATE Countries
            SET Name = @Name,
                IsDeleted = @IsDeleted
            WHERE Id = @CountryId
        ";

        public const string UpdateIndustry = UseOrganizationDatabase + @"
            UPDATE Industries
            SET Name = @Name,
                IsDeleted = @IsDeleted
            WHERE Id = @IndustryId
        ";

        public const string UpdateOrganization = UseOrganizationDatabase + @"
            UPDATE Organizations
            SET OrganizationId = @OrganizationId,
                Name = @Name,
                Website = @Website,
                CountryId = @CountryId, 
                Description = @Description,
                Founded = @Founded,
                IndustryId = @IndustryId,
                NumberOfEmployees = @NumberOfEmployees,
                IsDeleted = @IsDeleted
            WHERE Id = @Id
        ";

        public const string UpdateUser = UseOrganizationDatabase + @"
            UPDATE Users
            SET FirstName = @FirstName,
                LastName = @LastName,
                Age = @Age,
                PhoneNumber = @PhoneNumber,
                Country = @Country,
                AccountId = @AccountId,
                IsDeleted = @IsDeleted
            WHERE Id = @UserId
        ";
    }
}
