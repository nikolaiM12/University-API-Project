using OrganizationAPI.Client.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Services;
using OrganizationAPI.Infrastructure.Database;
using OrganizationAPI.Infrastructure.Mapper;
using OrganizationAPI.Infrastructure.Repositories;
using OrganizationAPI.Infrastructure.Repository;

namespace OrganizationAPI.API.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnector, DbConnector>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<DbTableCreator>();
            services.AddScoped<DbConnector>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ISectorRepository, IndustryRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ISectorService, IndustryService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICsvParsingService, CsvParsingService>();
            services.AddScoped<IFileSystemService, FileSystemService>();
            services.AddScoped<IIdCacheService, IdCacheService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPdfDownloadService, PdfDownloadService>();
            services.AddScoped<IJsonGeneratorService, JsonGeneratorService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
        }
    }
}
