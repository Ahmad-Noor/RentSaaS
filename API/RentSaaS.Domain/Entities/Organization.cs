using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Organization : IEntity
{
    [Key]
    public long OrganizationId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; } 

    [Column(TypeName = "nvarchar(100)")] 
    public string? Name { get; set; }  
    public bool? IsActive { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; }
}
