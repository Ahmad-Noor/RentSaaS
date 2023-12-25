using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentSaaS.Domain.Entities;
public record User : IEntity
{
    [Column(TypeName ="nvarchar(20)")]
    public string? Code { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public required string FirstName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public required string LastName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public required string UserName { get; set; }
     
    [Column(TypeName = "nvarchar(100)")]
    public required string Password { get;  set; }

    [Column(TypeName = "nvarchar(100)")]
    public required string Email { get; set; }
    public bool IsActive { get; set; } = true;

    public Guid? RoleId { get; set; }

}
