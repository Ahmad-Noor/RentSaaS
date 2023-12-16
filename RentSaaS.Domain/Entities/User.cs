using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public record User : IEntity
{ 
    public string? Code { get;  init; }
    public required string FirstName { get;  init; }
    public required string LastName { get;  init; }
    public required string UserName { get;  init; }
    public required string Password { get;  init; }
    public required string Email { get;  init; }

   // [ForeignKey("Role")]
    public Guid? RoleId { get; init; }
   // public virtual Role Role { get; init; }



}