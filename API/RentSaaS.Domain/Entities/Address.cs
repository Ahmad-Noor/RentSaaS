using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;
public class Address : IEntity
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

    [Column(TypeName = "nvarchar(100)")]
    public string? Street { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Apartment { get; init; }

    [Column(TypeName = "nvarchar(15)")]
    public string? POBox { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Line2 { get;  init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Country { get; init; } = default;

    [Column(TypeName = "nvarchar(100)")]
    public string? City { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? State { get;  init; }

    [Column(TypeName = "nvarchar(15)")]
    public string? PostalCode { get;  init; } 
}