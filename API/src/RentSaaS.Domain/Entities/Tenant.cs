using RentSaaS.Domain.Base;
using RentSaaS.Domain.Common;
namespace RentSaaS.Domain.Entities;
public class Tenant : BaseEntity
{      
    public string? Name { get; set; }              
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }  
    public Guid? ContactId { get; init; } 
    public Guid? AddressId { get; init; }     
    public ICollection<Lease>? Leases { get; set; }


}