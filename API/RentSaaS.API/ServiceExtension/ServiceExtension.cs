using RentSaaS.Common;
using Common.Services;
using RentSaaS.Domain;
using RentSaaS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
namespace RentSaaS.API.ServiceExtension;
public static class ServiceExtension
{
    public static IServiceCollection AddRentSaaSContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //---------------------- Multitenancy Setting & SQL lite DB
        List<Tenant> tenants;


        string folder = Path.Combine(Environment.CurrentDirectory, "Data");
        string dbPath = Path.Combine(folder, "ConfigurationDB.db");
        if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }

        //services.AddIdentity<User, IdentityRole>()
        //        .AddEntityFrameworkStores<ConfigurationDBContext>()
        //        .AddDefaultUI() 
        //        .AddDefaultTokenProviders();


        services.AddEntityFrameworkSqlite()
                .AddDbContext<ConfigurationDBContext>(options => options.UseSqlite($"Data Source={dbPath};Cache=Shared"));
        // services.AddDbContext<ConfigurationDBContext>(options => options.UseSqlite($"Data Source={dbPath};Cache=Shared"));




        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDBContext>();
            if (dbContext.Database.GetPendingMigrations().Any()) { dbContext.Database.Migrate(); }
            tenants = [.. dbContext.Tenants];

            var defaultSaaSDB = dbContext.Tenants.FirstOrDefault(t => t.IsDefault == true);
            if (defaultSaaSDB.DBProvider == "MSSQL")
            {
                services.AddDbContext<RentSaaSDBContext>(options => options.UseSqlServer(defaultSaaSDB.ConnectionString));
            }
            //TODO: if (defaultSaaSDB.DBProvider == "PostgreSQL")
            //TODO: if (defaultSaaSDB.DBProvider == "MySQL")
            tenants.Where(t => !t.ConnectionString.IsNullOrEmpty()).ToList().ForEach(tenant =>
            {
                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RentSaaSDBContext>();
                    dbContext.Database.SetConnectionString(tenant.ConnectionString);
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
            });
        }

        return services;
    }
}
