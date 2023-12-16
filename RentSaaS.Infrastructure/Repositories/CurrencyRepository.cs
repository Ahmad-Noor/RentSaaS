using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces;

namespace RentSaaS.Infrastructure.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
