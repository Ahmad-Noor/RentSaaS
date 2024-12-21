using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Customer : IEntity
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

    [Column(TypeName = "nvarchar(20)")]
    public string? Code { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; }
    public bool? IsActive { get; init; } = true;
    public long? ContactId { get; init; }

    //[ForeignKey("Address")]
    public long AddressId { get; init; }
    //public virtual Address Address { get; init; }


}