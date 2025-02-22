using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class ApplicationAndLeadsRepository : Repository<ApplicationAndLeads>, IApplicationAndLeadsRepository
{
    public ApplicationAndLeadsRepository(RentSaaSDBContext dbContext, ILogger<ApplicationAndLeadsRepository> logger) : base(dbContext, logger)
    {
    }
}
