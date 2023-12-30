using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 

namespace RentSaaS.Common;
public class IdentityDB : IdentityDbContext<IdentityUser>
{
    //protected readonly IConfiguration Configuration; 

    public IdentityDB(DbContextOptions<IdentityDB> options) : base(options)
    {
        //Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //builder.UseSqlite(Configuration.GetConnectionString("IdentityDBConnectionStrings"));
 
        string folder = Path.Combine(Environment.CurrentDirectory, "Data");
        string dbPath = Path.Combine(folder, "IdentityDB.db");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        builder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUser>(entity => { entity.ToTable(name: "Identity.Users"); });
        builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Identity.Roles"); });
        builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("Identity.UserRoles"); });
        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("Identity.UserClaims"); });
        builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("Identity.UserLogins"); });
        builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("Identity.RoleClaims"); });
        builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("Identity.UserTokens"); });

        builder.Entity<Tenant>().HasData(
        new Tenant
        {
            TenantId = "RentSaas",
            Name = "RentSaas",
            DBProvider = "MSSQL",
            IsDefault = true,
            ConnectionString = "Data Source=localhost;Initial Catalog=RentSaaS;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
        }, new Tenant
        {
            TenantId = "SkyRealty1",
            Name = "Sky Realty1",
            DBProvider = "MSSQL",
            ConnectionString = "Data Source=localhost;Initial Catalog=SkyRealty1;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
        },
        new Tenant
        {
            TenantId = "SkyRealty2",
            Name = "Sky Realty2",
            DBProvider = "MSSQL",
            ConnectionString = "Data Source=localhost;Initial Catalog=SkyRealty2;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
        },
        new Tenant
        {
            TenantId = "SkyRealty 3",
            Name = "Sky Realty 3",
        },
        new Tenant
        {
            TenantId = "SkyRealty 4",
            Name = "Sky Realty 4",
        }
        );

    }
    public DbSet<Tenant> Tenants { get; set; }
}