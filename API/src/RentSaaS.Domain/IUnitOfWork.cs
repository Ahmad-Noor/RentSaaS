using RentSaaS.Domain.Interfaces.Repositories;
namespace RentSaaS.Domain;
public interface IUnitOfWork : IAsyncDisposable
{ 
    IAddressRepository AddressRepository { get; set; }
    ICompanyRepository CompanyRepository { get; set; }
    IPropertyRepository PropertyRepository { get; set; } 
    IExpenseRepository ExpenseRepository { get; set; }
    ILeaseRepository LeaseRepository { get; set; }
    IExpenseFileRepository ExpenseFileRepository { get; set; } 
    IAdvertisingRepository AdvertisingRepository { get; set; }
    IApplicationAndLeadsRepository ApplicationAndLeadsRepository { get; set; }
    ITenantRepository TenantRepository { get; set; }
    IMaintenanceRepository MaintenanceRepository { get; set; }
    IMaintenancePhotoRepository MaintenancePhotoRepository { get; set; }

    IRecordPaymentRepository RecordPaymentRepository { get; set; }

    IRecordPaymentFile RecordPaymentFileRepository { get; set; }
    Task<int> SaveChangesAsync();
    void Rollback(); 
}
