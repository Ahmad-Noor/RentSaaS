using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Common.Services;
using Common;

namespace RentSaaS.Common;
public class IdentityDBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
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
        builder.Entity<User>(entity => { entity.ToTable(name: "Identity.Users"); });
        builder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable("Identity.Roles"); });
        builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("Identity.UserLogin"); });
        builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("Identity.UserRoles"); });
        builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("Identity.UserClaims"); });
        builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("Identity.UserLogins"); });
        builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("Identity.RoleClaims"); });
        builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("Identity.UserTokens"); });

        builder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("9a22aacf-0a27-4584-a1d8-9f31a3fa5676"),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                IsActive = true,
                Email = "admin@rentsaas.com",
                PasswordHash = Password.HashPassword("admin") 
            });
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
    public virtual DbSet<User> Users { get; set; }
}