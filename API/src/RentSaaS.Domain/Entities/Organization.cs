using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Organization : IEntity
{
    [Key]
    public Guid OrganizationId { get; set; }

    [Column(TypeName = "nvarchar(100)")] 
    public string? Name { get; set; } 
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public bool? IsActive { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; } 
     
}
