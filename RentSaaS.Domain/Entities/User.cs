using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public record User : IEntity
{
    [Column(TypeName ="nvarchar(20)")]
    public string? Code { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public required string FirstName { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public required string LastName { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public required string UserName { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public required string Password { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public required string Email { get;  init; }

}
