using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities; 

namespace RentSaaS.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
