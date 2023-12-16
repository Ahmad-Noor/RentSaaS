using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces;

namespace RentSaaS.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
