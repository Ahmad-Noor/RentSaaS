using RentSaaS.Domain.Base; 
namespace RentSaaS.Domain.Entities;
public class Tenant : IEntity
{
    public long TenantId { get; set; }        
    public string? Name { get; set; }              
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; } 
    public bool? IsActive { get; init; } = true;
    public Guid? ContactId { get; init; } 
    public Guid? AddressId { get; init; }
    public long OrganizationId { get; set; }        
    public Organization Organization { get; set; } 
    public ICollection<Lease>? Leases { get; set; }


}