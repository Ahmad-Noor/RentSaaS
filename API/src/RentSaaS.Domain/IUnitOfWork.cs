using RentSaaS.Domain.Interfaces.Repositories;
namespace RentSaaS.Domain;
public interface IUnitOfWork : IAsyncDisposable
{ 
    IAddressRepository AddressRepository { get; set; } 
     
    Task<int> SaveChangesAsync();
    void Rollback(); 
}
