using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
namespace RentSaaS.Common;
public class TenantService : ITenantService
{
    private readonly ConfigurationDBContext _configurationDB;
    private readonly HttpContext? _httpContext;
    private Tenant? _currenttenant;
    public TenantService(IHttpContextAccessor httpContextAccessor, IOptions<ConfigurationDBContext>  identityDB)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _configurationDB = identityDB.Value;
        if (_httpContext is not null)
        {
            if (_httpContext!.Request.Headers.TryGetValue("tenant", out var tenantId))
            {
                //todo: logoer
                SetCurrentTenant(tenantId);
            }
            else
            {
                //todo: logoer
                throw new UnauthorizedAccessException("No tenant provided!");
            }
        }
    }

    private void SetCurrentTenant(string tenantId)
    {
        _currenttenant = _configurationDB.Tenants.FirstOrDefault(c => c.TenantId == tenantId);
        if (_currenttenant is null)
        {
            throw new UnauthorizedAccessException("Invalid tenant Id!");
        }
        if (string.IsNullOrEmpty(_currenttenant.ConnectionString))
        {
            _currenttenant.ConnectionString = GetDefualtConnectionString();
        }
    }
     
    public string? GetConnectionString()
    {
        var connectionString = _currenttenant is null ? GetDefualtConnectionString() : _currenttenant.ConnectionString;
        return connectionString;
    }

    public Tenant? GetCurrentTenant()
    {
        return _currenttenant;
    }
    private string? GetDefualtConnectionString()
    {
        return _configurationDB.Tenants.FirstOrDefault(c => c.IsDefault == true).ConnectionString;
    }

    public string? GetDatabaseProvider()
    {
        return _configurationDB.Tenants.FirstOrDefault(c => c.IsDefault == true).DBProvider;
    }
}