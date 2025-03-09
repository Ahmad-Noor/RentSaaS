using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories
{


    public class AdvertisingFileRepository : Repository<AdvertisingFile>, IAdvertisingFileRepository
    {
        public AdvertisingFileRepository(RentSaaSDBContext dbContext, ILogger<AdvertisingFileRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
