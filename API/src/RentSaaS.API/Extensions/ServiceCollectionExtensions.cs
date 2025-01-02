using Microsoft.EntityFrameworkCore;
using RentSaaS.Application.Services.Implementations;
using RentSaaS.Application.Services.Interfaces;
using RentSaaS.Domain;
using RentSaaS.Infrastructure.Data;

namespace RentSaaS.API.Extensions
{
    /// <summary>
    /// Provides extension methods for registering RentSaaS-related services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the RentSaaS context, associated services, and applies pending EF Migrations on startup.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register domain services
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 


            //services.AddScoped<INotificationService, NotificationService>();
            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IUserService, UserService>();
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
 
             

            // Register the DbContext with a SQL Server provider
            services.AddDbContext<RentSaaSDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RentSaaSDB")));

            // Uncomment the following lines to only apply migrations in certain environments, for example:
            //
            // var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // if (!string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase))
            // {
            //     // Apply pending migrations if not in Production
            // }

            // Apply pending migrations at startup
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RentSaaSDBContext>();
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
 
                }
                catch (Exception ex)
                {
                    // Optional: log or handle migration errors
                    // var logger = scope.ServiceProvider.GetRequiredService<ILogger<ServiceCollectionExtensions>>();
                    // logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }

            return services;
        }
    }
}