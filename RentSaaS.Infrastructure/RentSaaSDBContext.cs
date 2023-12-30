using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentSaaS.Common;
using RentSaaS.Domain.Base;
using RentSaaS.Domain.Entities;

namespace RentSaaS.Infrastructure;

public class RentSaaSDBContext : DbContext
{
    //private readonly string connectionString;
    public string TenantId { get; set; }
    private readonly ITenantService _tenantService;

    public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public RentSaaSDBContext(DbContextOptions<RentSaaSDBContext> options, ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
        TenantId = _tenantService.GetCurrentTenant()?.TenantId;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string tenantConnectionString = _tenantService.GetConnectionString();
        if (!string.IsNullOrWhiteSpace(tenantConnectionString))
        {
            var dbProvider = _tenantService.GetDatabaseProvider();

            if (string.Equals(dbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).UseSqlServer(tenantConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                                                        {
                                                            sqlOptions.EnableRetryOnFailure(
                                                                maxRetryCount: 10,
                                                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                errorNumbersToAdd: null);
                                                        });
            }
            //else if (string.Equals(dbProvider, "MySQL", StringComparison.OrdinalIgnoreCase))
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //todo: https://www.youtube.com/watch?v=tsWXmKfqHE4&list=PL62tSREI9C-dugbPn185_D6fSzIBC0LK3&index=7&ab_channel=DevCreed
        //modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        //modelBuilder.Entity<User>().HasQueryFilter(w => w.TenantId == TenantId);
        base.OnModelCreating(builder);

        builder.ApplyGlobalFilters<IEntity>(e => e.TenantId == TenantId);

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added))
        {
            entry.Entity.TenantId = TenantId;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Customer> Customers { get; set; }

}
