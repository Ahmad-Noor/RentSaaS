using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Role : BaseEntity
{ 

    [Column(TypeName = "nvarchar(100)")]
    public required string Name { get; init; }
    //public virtual ICollection<User> Users { get; init; }
}