using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
namespace RentSaaS.Common;

public class TenantService : ITenantService
{
    private readonly IdentityDB _identityDB;
    private readonly HttpContext? _httpContext;
    private Tenant? _currenttenant;
    public TenantService(IHttpContextAccessor httpContextAccessor, IOptions<IdentityDB>  identityDB)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _identityDB = identityDB.Value;
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
        _currenttenant = _identityDB.Tenants.FirstOrDefault(c => c.TenantId == tenantId);
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
        return _identityDB.Tenants.FirstOrDefault(c => c.IsDefault == true).ConnectionString;
    }

    public string? GetDatabaseProvider()
    {
        return _identityDB.Tenants.FirstOrDefault(c => c.IsDefault == true).DBProvider;
    }
}