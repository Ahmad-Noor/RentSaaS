using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public record Currency : IEntity
{
    [Column(TypeName = "nvarchar(20)")]
    public string? Code { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; }
    public float? Rate { get; init; }

    [Column(TypeName = "nvarchar(5)")]
    public string? Symbol { get; init; }

}