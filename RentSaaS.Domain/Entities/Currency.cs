using RentSaaS.Domain.Base;

namespace RentSaaS.Domain.Entities;
public record Currency : IEntity
{
    public string? Code { get; init; }
    public string Name { get; init; }
    public float? Rate { get; init; }
    public string? Symbol { get; init; }

}