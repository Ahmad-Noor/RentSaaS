using RentSaaS.Domain.Entities;

namespace RentSaaS.Application.Services;
public interface IOrganizationService
{
    //string? GetDatabaseProvider();
    //string? GetConnectionString();
    Organization? GetCurrentOrganization();
}