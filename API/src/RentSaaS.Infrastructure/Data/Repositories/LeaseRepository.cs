using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class LeaseRepository : Repository<Lease>, ILeaseRepository
{
    public LeaseRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
