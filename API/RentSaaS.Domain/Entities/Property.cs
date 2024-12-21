using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Property : IEntity
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public long OrganizationId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; }
    public long PropertyId { get; set; }  
    public string? Address { get; set; }   
    public ICollection<Lease>? Leases { get; set; }  

}