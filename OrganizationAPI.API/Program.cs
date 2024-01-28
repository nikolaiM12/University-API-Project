using OrganizationAPI.API.Extensions;
using OrganizationAPI.API.Middleware;
using OrganizationAPI.Infrastructure.Mapper;

namespace OrganizationAPI.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ServiceConfigurationExtensions.ConfigureDatabaseServices(builder.Services);
            ServiceConfigurationExtensions.ConfigureRepositories(builder.Services);
            ServiceConfigurationExtensions.ConfigureServices(builder.Services);

            builder.AddJwtAuthentication();

            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            SwaggerServiceExtensions.AddSwagger(builder.Services);

            await InitializeDatabaseExtensions.InitializeDatabase(builder.Services.BuildServiceProvider());

            await ProccessDataExtensions.ProcessData(builder.Services.BuildServiceProvider());

            var app = builder.Build();

            app.UseMiddleware<IPFilterMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}