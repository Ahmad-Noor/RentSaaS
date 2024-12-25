using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
