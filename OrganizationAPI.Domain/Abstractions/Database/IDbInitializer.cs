namespace OrganizationAPI.Domain.Abstractions.Database
{
    public interface IDbInitializer
    {
        Task CreateDatabase();
    }
}
