using Common.Services;
namespace RentSaaS.Common;

public interface ITenantService
{
    string? GetDatabaseProvider();
    string? GetConnectionString();
    Tenant? GetCurrentTenant();
}