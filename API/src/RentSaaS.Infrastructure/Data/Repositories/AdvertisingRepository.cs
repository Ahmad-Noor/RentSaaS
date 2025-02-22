using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class AdvertisingRepository : Repository<Advertising>, IAdvertisingRepository
{
    public AdvertisingRepository(RentSaaSDBContext dbContext, ILogger<AdvertisingRepository> logger) : base(dbContext, logger)
    {
    }
}
