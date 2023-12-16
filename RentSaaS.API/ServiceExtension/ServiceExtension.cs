using Microsoft.EntityFrameworkCore;
using RentSaaS.Common;
using RentSaaS.Domain;
using RentSaaS.Infrastructure;

namespace RentSaaS.API.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<TenantSettings>(configuration.GetSection(nameof(TenantSettings)));

            TenantSettings tenantSettings = new();
            configuration.GetSection(nameof(TenantSettings)).Bind(tenantSettings);

            var defaultDBProvider = tenantSettings.Defaults?.DBProvider;
            if (defaultDBProvider == "MSSQL")
            {
                services.AddDbContext<RentSaaSDBContext>(options => options.UseSqlServer(tenantSettings.Defaults.ConnectionString));
            }

            foreach (var tenant in tenantSettings.Tenants)
            {
                var connectString = tenant.ConnectionString ?? tenantSettings.Defaults.ConnectionString;
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<RentSaaSDBContext>();
                dbContext.Database.SetConnectionString(connectString);
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            return services;
        }
    }
}
