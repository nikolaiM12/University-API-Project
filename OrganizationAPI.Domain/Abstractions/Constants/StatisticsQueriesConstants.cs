namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class StatisticsQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string GetCompaniesByEmployeesDescending = UseOrganizationDatabase + @"
            SELECT Name, NumberOfEmployees
            FROM Organizations
            ORDER BY NumberOfEmployees DESC
        ";

        public const string GetTotalEmployeesByIndustry = UseOrganizationDatabase + @"
            SELECT IndustryId, SUM(NumberOfEmployees) AS TotalEmployees
            FROM Organizations
            GROUP BY IndustryId
        ";

        public const string GetTotalEmployeesByCountryAndIndustry = UseOrganizationDatabase + @"
            SELECT CountryId, IndustryId, SUM(NumberOfEmployees) AS TotalEmployees
            FROM Organizations
            GROUP BY CountryId, IndustryId
        ";

        public const string GetCompaniesWithMostEmployees = UseOrganizationDatabase + @"
            SELECT TOP (@TopN) Name, NumberOfEmployees
            FROM Organizations
        ";

        public const string GetComplexStatistics = UseOrganizationDatabase + @"
            SELECT 
                C.Name AS CountryName,
                I.Name AS IndustryName,
                SUM(O.NumberOfEmployees) AS TotalEmployees
            FROM Organizations O
            INNER JOIN Countries C ON O.CountryId = C.Id
            INNER JOIN Industries I ON O.IndustryId = I.Id
            WHERE O.IsDeleted = 0
            GROUP BY C.Name, I.Name
        ";
    }
}
