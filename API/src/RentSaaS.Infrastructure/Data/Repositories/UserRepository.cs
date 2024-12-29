using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
