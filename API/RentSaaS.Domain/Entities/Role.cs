using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public record Role : IEntity
{
    [Column(TypeName = "nvarchar(20)")]
    public string? Code { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; }
    //public virtual ICollection<User> Users { get; init; }
}