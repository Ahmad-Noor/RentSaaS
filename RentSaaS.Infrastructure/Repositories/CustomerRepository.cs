using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces;

namespace RentSaaS.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
