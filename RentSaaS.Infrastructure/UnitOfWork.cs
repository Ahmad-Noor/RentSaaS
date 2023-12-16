using RentSaaS.Domain;
using RentSaaS.Domain.Base;
using Microsoft.Extensions.Logging;
using RentSaaS.Infrastructure.Repositories;
using RentSaaS.Domain.Interfaces;
using RentSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RentSaaS.Infrastructure;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly RentSaaSDBContext _dbContext;
    private readonly ILogger _logger;

    public IBranchRepository Branchs { get;  set; }
    public IAddressRepository Addresses { get;  set; }
    public ICurrencyRepository Currencies { get;  set; }
    public ICustomerRepository Customers { get;  set; }
    public IUserRepository Users { get;  set; }
    public IRoleRepository Roles { get;  set; }

    public UnitOfWork(RentSaaSDBContext dbContext, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("logs");
        _dbContext = dbContext;

        Branchs = new BranchRepository(dbContext, _logger);
        Addresses = new AddressRepository(dbContext, _logger);
        Currencies = new CurrencyRepository(dbContext, _logger);
        Customers = new CustomerRepository(dbContext, _logger);
        Users = new UserRepository(dbContext, _logger);
        Roles = new RoleRepository(dbContext, _logger);
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

    //ValueTask IAsyncDisposable.DisposeAsync() { dbContext.DisposeAsync(); GC.SuppressFinalize(this); }


}
