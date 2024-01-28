namespace OrganizationAPI.Domain.Abstractions.Constants
{
    public class InnerJoinQueriesConstants
    {
        public const string UseOrganizationDatabase = "USE organization";

        public const string OrganizationDetailsJoinQuery = UseOrganizationDatabase + @"
            SELECT o.Id, o.OrganizationId, o.Name, o.Website, c.Name AS Country, o.Description, o.Founded, i.Name AS Industry, o.NumberOfEmployees
            FROM Organizations AS o
            INNER JOIN Countries AS c ON c.Id = o.CountryId
            INNER JOIN Industries AS i ON i.Id = o.IndustryId
        ";
    }
}
