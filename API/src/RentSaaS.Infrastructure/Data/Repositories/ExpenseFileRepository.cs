using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class ExpenseFileRepository : Repository<ExpenseFile>, IExpenseFileRepository
{
    public ExpenseFileRepository(RentSaaSDBContext dbContext, ILogger<ExpenseFileRepository> logger) : base(dbContext, logger)
    {
    }
}
