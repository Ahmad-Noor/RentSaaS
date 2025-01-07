using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class ExpenseRepository : Repository<Expense>, IExpenseRepository
{
    public ExpenseRepository(RentSaaSDBContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }
}
