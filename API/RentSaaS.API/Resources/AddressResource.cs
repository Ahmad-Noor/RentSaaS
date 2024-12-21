namespace RentSaaS.API.Resources;

public class AddressResource
{
    public required long Id { get; set; }
    public string OrganizationId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? Note { get; set; }

    public string Street { get; init; }
    public string Apartment { get; init; }
    public string? POBox { get; init; }
    public string? Line2 { get; init; }
    public string? Country { get; init; } = default;
    public string City { get; init; }
    public string State { get; init; }
    public string? PostalCode { get; init; }
    public bool IsActive { get; init; } = true;
}