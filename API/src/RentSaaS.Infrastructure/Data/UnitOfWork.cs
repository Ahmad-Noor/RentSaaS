using RentSaaS.Domain; 
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; 
using RentSaaS.Domain.Interfaces.Repositories;
using RentSaaS.Infrastructure.Data.Repositories;
namespace RentSaaS.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly RentSaaSDBContext _dbContext;
    private readonly ILogger _logger;
     
    public IAddressRepository AddressRepository { get; set; } 
    public ICompanyRepository CompanyRepository { get; set; } 
    public IPropertyRepository PropertyRepository { get; set; } 
    public IExpenseRepository ExpenseRepository { get; set; } 
    public ILeaseRepository LeaseRepository { get; set; } 

    public UnitOfWork(RentSaaSDBContext dbContext, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("logs");
        _dbContext = dbContext;
         
        AddressRepository = new AddressRepository(dbContext, _logger);
        CompanyRepository = new CompanyRepository(dbContext, _logger);
        PropertyRepository = new PropertyRepository(dbContext, _logger);
        ExpenseRepository = new ExpenseRepository(dbContext, _logger); 
        LeaseRepository = new LeaseRepository(dbContext, _logger); 
    }

    //public IRepository<T> AsyncRepository<T>() where T : IEntity
    //{
    //    return new Repository<T>(_dbContext, _logger);
    //}

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
    public void Rollback()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

}
