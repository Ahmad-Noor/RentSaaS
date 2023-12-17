using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public record Customer : IEntity
{
    [Column(TypeName = "nvarchar(20)")]
    public string? Code { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; }
    public bool? IsActive { get; init; } = true;
    public Guid? ContactId { get; init; }

    //[ForeignKey("Address")]
    public Guid AddressId { get; init; }
    //public virtual Address Address { get; init; }


}