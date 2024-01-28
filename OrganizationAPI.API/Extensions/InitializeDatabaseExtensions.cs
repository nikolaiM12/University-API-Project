using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Infrastructure.Database;

namespace OrganizationAPI.API.Extensions
{
    public static class InitializeDatabaseExtensions
    {
        public static async Task InitializeDatabase(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    var dbTableCreator = scope.ServiceProvider.GetRequiredService<DbTableCreator>();

                    await dbInitializer.CreateDatabase();
                    await dbTableCreator.CreateTables();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
