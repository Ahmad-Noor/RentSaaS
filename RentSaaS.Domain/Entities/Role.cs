using RentSaaS.Domain.Base;
 
namespace RentSaaS.Domain.Entities;
public record Role : IEntity
{ 
    public string? Code { get; init; }
    public string Name { get; init; }
    //public virtual ICollection<User> Users { get; init; }

}
