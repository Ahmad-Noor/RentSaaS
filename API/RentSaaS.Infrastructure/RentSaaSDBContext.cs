using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Base;
using RentSaaS.Domain.Entities;
using RentSaaS.Infrastructure.Services;

namespace RentSaaS.Infrastructure;

public class RentSaaSDBContext : DbContext
{
    //private readonly string connectionString;
    //public long OrganizationId { get; set; }
    private readonly IOrganizationService _organizationService;

    public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public RentSaaSDBContext() { }
    public RentSaaSDBContext(DbContextOptions<RentSaaSDBContext> options) : base(options)
    { 
    }
    public RentSaaSDBContext(DbContextOptions<RentSaaSDBContext> options, IOrganizationService OrganizationService) : base(options)
    {
        _organizationService = OrganizationService;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=localhost;Initial Catalog=RentSaaS_Dev;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";

        optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).UseSqlServer(connectionString,
            sqlServerOptionsAction: sqlOptions =>
                                                {
                                                    sqlOptions.EnableRetryOnFailure(
                                                        maxRetryCount: 10,
                                                        maxRetryDelay: TimeSpan.FromSeconds(30),
                                                        errorNumbersToAdd: null);
                                                });

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //todo: https://www.youtube.com/watch?v=tsWXmKfqHE4&list=PL62tSREI9C-dugbPn185_D6fSzIBC0LK3&index=7&ab_channel=DevCreed
        //modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        if (_organizationService != null)
        { 
              builder.ApplyGlobalFilters<IEntity>(e => e.OrganizationId == _organizationService.GetCurrentOrganization()!.OrganizationId);
        }

        builder.Entity<User>(entity => { entity.ToTable(name: "Identity.Users"); });
        builder.Entity<IdentityRole<long>>(entity => { entity.ToTable("Identity.Roles"); });

        builder.Entity<IdentityUserLogin<long>>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
        builder.Entity<IdentityUserLogin<long>>(entity => { entity.ToTable("Identity.UserLogin"); });

        builder.Entity<IdentityUserRole<long>>(entity => { entity.ToTable("Identity.UserRoles"); });
        builder.Entity<IdentityUserRole<long>>().HasKey(r => new { r.UserId, r.RoleId });

        builder.Entity<IdentityUserClaim<long>>(entity => { entity.ToTable("Identity.UserClaims"); });
        builder.Entity<IdentityRoleClaim<long>>(entity => { entity.ToTable("Identity.RoleClaims"); });

        builder.Entity<IdentityUserToken<long>>(entity => { entity.ToTable("Identity.UserTokens"); });
        builder.Entity<IdentityUserToken<long>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });


        builder.Entity<Organization>().HasData(
            new Organization
            {
                OrganizationId = 1,
                Name = "Organization 1",
                IsActive = true,
            });


        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                IsActive = true,
                Email = "admin@rentsaas.com",
                PasswordHash = Password.HashPassword("admin"),
                OrganizationId = 1
            });
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added))
        {
            entry.Entity.OrganizationId = _organizationService.GetCurrentOrganization()!.OrganizationId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }

}
