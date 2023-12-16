using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Base;
using RentSaaS.Domain.Interfaces;
namespace RentSaaS.Domain;
public interface IUnitOfWork : IAsyncDisposable
{
    IBranchRepository Branchs { get;   set; }
    IAddressRepository Addresses { get;   set; }
    ICurrencyRepository Currencies { get;   set; }
    ICustomerRepository Customers { get;   set; }
    IUserRepository Users { get;   set; }
    IRoleRepository Roles { get;   set; }



    Task<int> CompleteAsync();
    void Rollback();
    IRepository<T> AsyncRepository<T>() where T : IEntity;
}
