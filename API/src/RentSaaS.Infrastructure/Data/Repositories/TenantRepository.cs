using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
