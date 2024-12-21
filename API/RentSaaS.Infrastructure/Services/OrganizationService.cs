using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace RentSaaS.Infrastructure.Services;
public class OrganizationService : IOrganizationService
{
    private readonly HttpContext? _httpContext;
    private Organization? _currentOrganization; 

    public OrganizationService(IHttpContextAccessor httpContextAccessor )
    {
        _httpContext = httpContextAccessor.HttpContext; 

        if (_httpContext is not null)
        {
            if (_httpContext!.Request.Headers.TryGetValue("X-OrganizationId", out var organizationIdHeader))
            {
                if (long.TryParse(organizationIdHeader, out long organizationId))
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

    public void SetCurrentOrganization(long organizationId)
    {
         _currentOrganization =new Organization { OrganizationId = organizationId };    
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