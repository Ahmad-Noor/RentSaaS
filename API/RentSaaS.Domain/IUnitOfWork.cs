using RentSaaS.Domain.Interfaces;
namespace RentSaaS.Domain;
public interface IUnitOfWork : IAsyncDisposable
{
    IBranchRepository BranchRepository { get; set; }
    IAddressRepository AddressRepository { get; set; }
    ICurrencyRepository CurrencyRepository { get; set; }
    ICustomerRepository CustomerRepository { get; set; }


    Task<int> CompleteAsync();
    void Rollback();
    //IRepository<T> AsyncRepository<T>() where T : IEntity;
}
