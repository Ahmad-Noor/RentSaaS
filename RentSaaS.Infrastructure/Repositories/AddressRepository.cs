using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces;

namespace RentSaaS.Infrastructure.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
