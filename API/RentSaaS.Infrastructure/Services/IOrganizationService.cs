using RentSaaS.Domain.Entities;

namespace RentSaaS.Infrastructure.Services;
public interface IOrganizationService
{
    //string? GetDatabaseProvider();
    //string? GetConnectionString();
   Organization? GetCurrentOrganization();
}