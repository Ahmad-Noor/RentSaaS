using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using RentSaaS.Domain.Entities;
using RentSaaS.Infrastructure.Services;
using RentSaaS.Infrastructure;

public class RentSaaSDbContextFactory : IDesignTimeDbContextFactory<RentSaaSDBContext>
{
    public RentSaaSDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RentSaaSDBContext>();

        string connectionString = "Data Source=localhost;Initial Catalog=RentSaaS_Dev;Persist Security Info=True;User ID=sa;Password=sa;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
        optionsBuilder.UseSqlServer(connectionString);
         

        return new RentSaaSDBContext(optionsBuilder.Options);
    }
} 