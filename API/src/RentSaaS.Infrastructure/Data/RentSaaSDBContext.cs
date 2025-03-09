using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentSaaS.Application.Services;
using RentSaaS.Domain.Base;
using RentSaaS.Domain.Common;
using RentSaaS.Domain.Entities;
using System.Reflection;

namespace RentSaaS.Infrastructure.Data;

public class RentSaaSDBContext : DbContext
{
    //private readonly string connectionString;
    //public Guid OrganizationId { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //todo: https://www.youtube.com/watch?v=tsWXmKfqHE4&list=PL62tSREI9C-dugbPn185_D6fSzIBC0LK3&index=7&ab_channel=DevCreed
        //modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        if (_organizationService != null)
        {
            modelBuilder.ApplyGlobalFilters<IEntity>(e => e.OrganizationId == _organizationService.GetCurrentOrganization()!.OrganizationId);
        }

        modelBuilder.Entity<User>(entity => { entity.ToTable(name: "Identity.Users"); });
        modelBuilder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable("Identity.Roles"); });

        modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("Identity.UserLogin"); });

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("Identity.UserRoles"); });
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(r => new { r.UserId, r.RoleId });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("Identity.UserClaims"); });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("Identity.RoleClaims"); });

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("Identity.UserTokens"); });
        modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        var organizationId = new Guid("00000000-0000-0000-0000-000000000001");
        modelBuilder.Entity<Organization>().HasData(
            new Organization
            {
                OrganizationId = organizationId,
                Name = "Organization 1",
                IsActive = true,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000")
            });


        modelBuilder.Entity<User>().HasData(
            new User
            {
                //Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                IsActive = true,
                Email = "admin@rentsaas.com",
                PasswordHash = Password.HashPassword("admin"),
                OrganizationId = organizationId,
                UserType = "Landlord",
            });
        //ExpenseCategory.Create("Maintenance", "Property maintenance expenses", "#FF5733", "wrench", null, true, 1),
        //ExpenseCategory.Create("Utilities", "Utility bills and services", "#33FF57", "bolt", null, true, 2),
        //ExpenseCategory.Create("Insurance", "Property insurance expenses", "#3357FF", "shield", null, true, 3),
        //ExpenseCategory.Create("Taxes", "Property taxes and related expenses", "#FF33F5", "file-invoice-dollar", null, true, 4),
        //ExpenseCategory.Create("Marketing", "Marketing and advertising expenses", "#33FFF5", "bullhorn", null, true, 5),
        //ExpenseCategory.Create("Administrative", "Administrative and office expenses", "#F5FF33", "building", null, true, 6),
        //ExpenseCategory.Create("Legal", "Legal and professional fees", "#FF3333", "gavel", null, true, 7),
        //ExpenseCategory.Create("Other", "Miscellaneous expenses", "#808080", "ellipsis-h", null, true, 8)




        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added))
        {
            entry.Entity.OrganizationId = _organizationService.GetCurrentOrganization()!.OrganizationId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Lease> leases { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseFile> expenseFiles { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Advertising> Advertising { get; set; }
    public DbSet<AdvertisingFile> AdvertisingFiles { get; set; }

    public DbSet<ApplicationAndLeads> ApplicationAndLeads { get; set; }

    public DbSet<RecordPayment> RecordPayments { get; set; }
    public DbSet<RecordPaymentFile> RecordPaymentFiles { get; set; }
    public DbSet<Maintenance> Maintenance { get; set; }
    public DbSet<MaintenancePhoto> MaintenancePhoto { get; set; }
    public DbSet<Tenant> Tenants { get; set; }



}
