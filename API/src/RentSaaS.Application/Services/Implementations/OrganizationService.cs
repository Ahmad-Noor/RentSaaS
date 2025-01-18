using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using RentSaaS.Application.Services.Interfaces;
using Microsoft.Extensions.Primitives;

namespace RentSaaS.Application.Services.Implementations;
public class OrganizationService : IOrganizationService
{
    private readonly HttpContext? _httpContext;
    private Organization? _currentOrganization;

    public OrganizationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;

        if (_httpContext is not null)
        {
            if (!httpContextAccessor.HttpContext.Request.Path.ToString().Contains("/api/Auth/"))
            {
                if (_httpContext!.Request.Headers.TryGetValue("X-OrganizationId", out StringValues organizationIdHeader))
                {
                    if (Guid.TryParse(organizationIdHeader.ToString(), out Guid organizationId))
                    {
                        SetCurrentOrganization(organizationId);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Invalid Organization Id format!");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("No Organization provided!");
                }
            }
        }

    }

    public void SetCurrentOrganization(Guid organizationId)
    {
        _currentOrganization = new Organization { OrganizationId = organizationId };
        if (_currentOrganization is null)
        {
            throw new UnauthorizedAccessException("Invalid Organization Id!");
        }
    }

    public Organization? GetCurrentOrganization()
    {
        return _currentOrganization;
    }

}
