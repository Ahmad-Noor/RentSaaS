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
    public IExpenseFileRepository ExpenseFileRepository { get; set; } 

    public IAdvertisingRepository AdvertisingRepository { get; set; }
    public IApplicationAndLeadsRepository ApplicationAndLeadsRepository { get; set; }

    public IRecordPaymentRepository RecordPaymentRepository { get; set; }

    public IRecordPaymentFile RecordPaymentFileRepository { get; set; }
    public IMaintenanceRepository MaintenanceRepository { get; set; }
    public IMaintenancePhotoRepository MaintenancePhotoRepository { get; set; }

    public ITenantRepository tenantRepository { get; set; }



    public UnitOfWork(RentSaaSDBContext dbContext, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("logs");
        _dbContext = dbContext;
         
        AddressRepository = new AddressRepository(dbContext, loggerFactory.CreateLogger<AddressRepository>());
        CompanyRepository = new CompanyRepository(dbContext, loggerFactory.CreateLogger<CompanyRepository>());
        PropertyRepository = new PropertyRepository(dbContext, loggerFactory.CreateLogger<PropertyRepository>());
        ExpenseRepository = new ExpenseRepository(dbContext, loggerFactory.CreateLogger<ExpenseRepository>());
        LeaseRepository = new LeaseRepository(dbContext, loggerFactory.CreateLogger<LeaseRepository>());
        ExpenseFileRepository = new ExpenseFileRepository(dbContext, loggerFactory.CreateLogger<ExpenseFileRepository>());
        AdvertisingRepository =new AdvertisingRepository(dbContext, loggerFactory.CreateLogger<AdvertisingRepository>());
        ApplicationAndLeadsRepository = new ApplicationAndLeadsRepository(dbContext, loggerFactory.CreateLogger<ApplicationAndLeadsRepository>());
        RecordPaymentRepository = new RecordPaymentRepository(dbContext,loggerFactory.CreateLogger<RecordPaymentRepository>());
        RecordPaymentFileRepository = new RecordPaymentFileRepository(dbContext, loggerFactory.CreateLogger<RecordPaymentFileRepository>());
        MaintenanceRepository=new MaintenanceRepository(dbContext, loggerFactory.CreateLogger<MaintenanceRepository>());
        MaintenancePhotoRepository=new MaintenancePhotoRepository(dbContext, loggerFactory.CreateLogger<MaintenancePhotoRepository>());

        tenantRepository = new TenantRepository(dbContext, loggerFactory.CreateLogger<TenantRepository>());
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
