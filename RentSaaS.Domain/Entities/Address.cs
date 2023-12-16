using RentSaaS.Domain.Base;

namespace RentSaaS.Domain.Entities;
public record Address : IEntity
{ 
    public string? Street { get; init; }
    public string? Apartment { get; init; }
    public string? POBox { get; init; }
    public string? Line2 { get;  init; }
    public string? Country { get; init; } = default;
    public string? City { get; init; }
    public string? State { get;  init; }
    public string? PostalCode { get;  init; }
}