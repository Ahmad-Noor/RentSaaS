using RentSaaS.Domain.Entities;
using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(RentSaaSDBContext dbContext, ILogger<TenantRepository> logger) : base(dbContext, logger)
    {
    }
}
