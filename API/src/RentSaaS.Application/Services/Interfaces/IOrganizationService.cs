using RentSaaS.Domain.Entities;

namespace RentSaaS.Application.Services.Interfaces;
public interface IOrganizationService
{
    //string? GetDatabaseProvider();
    //string? GetConnectionString();
    Organization? GetCurrentOrganization();
}