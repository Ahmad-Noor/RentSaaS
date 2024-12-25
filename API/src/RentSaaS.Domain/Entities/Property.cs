using RentSaaS.Domain.Base;
using RentSaaS.Domain.Common; 
namespace RentSaaS.Domain.Entities;
public class Property : BaseEntity
{   
    public Guid PropertyId { get; set; }  
    public string? Address { get; set; }   
    public ICollection<Lease>? Leases { get; set; }  

}