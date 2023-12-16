using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace RentSaaS.Common;

public class TenantService : ITenantService
{
    private readonly TenantSettings _tenantSettings;
    private HttpContext? _httpContext;
    private Tenant? _currenttenant;
    public TenantService(IHttpContextAccessor httpContextAccessor, IOptions<TenantSettings> tenantSettings)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _tenantSettings = tenantSettings.Value;
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
        _currenttenant = _tenantSettings.Tenants.FirstOrDefault(c => c.TenantId == tenantId);
        if (_currenttenant is null)
        {
            throw new UnauthorizedAccessException("Invalid tenant Id!");
        }
        if (string.IsNullOrEmpty(_currenttenant.ConnectionString))
        {
            _currenttenant.ConnectionString = _tenantSettings!.Defaults!.ConnectionString;
        }
    }

    public string? GetConnectionString()
    {
        var connectionString = _currenttenant is null ? _tenantSettings!.Defaults!.ConnectionString : _currenttenant.ConnectionString;
        return connectionString;
    }

    public Tenant? GetCurrentTenant()
    {
        return _currenttenant;
    }

    public string? GetDatabaseProvider()
    {
        return _tenantSettings!.Defaults!.DBProvider;
    }
}