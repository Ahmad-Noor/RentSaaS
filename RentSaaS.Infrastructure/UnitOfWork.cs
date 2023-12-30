using RentSaaS.Domain;
using RentSaaS.Domain.Base;
using RentSaaS.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RentSaaS.Infrastructure.Repositories;

namespace RentSaaS.Infrastructure;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly RentSaaSDBContext _dbContext;
    private readonly ILogger _logger;

    public IBranchRepository BranchRepository { get;  set; }
    public IAddressRepository AddressRepository { get;  set; }
    public ICurrencyRepository CurrencyRepository { get;  set; }
    public ICustomerRepository CustomerRepository { get;  set; }

    public UnitOfWork(RentSaaSDBContext dbContext, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("logs");
        _dbContext = dbContext;

        BranchRepository = new BranchRepository(dbContext, _logger);
        AddressRepository = new AddressRepository(dbContext, _logger);
        CurrencyRepository = new CurrencyRepository(dbContext, _logger);
        CustomerRepository = new CustomerRepository(dbContext, _logger);
    }

    public IRepository<T> AsyncRepository<T>() where T : IEntity
    {
        return new Repository<T>(_dbContext, _logger);
    }

    public async Task<int> CompleteAsync()
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
