using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public record Customer : IEntity
{
    public string? Code { get; init; }
    public string Name { get; init; }
    public bool? IsActive { get; init; } = true;
    public Guid? ContactId { get; init; }

    //[ForeignKey("Address")]
    public Guid AddressId { get; init; }
    //public virtual Address Address { get; init; }


}