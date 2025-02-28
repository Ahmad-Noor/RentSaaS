namespace RentSaaS.Application.DTOs.Address
{
    public class AddressGetDto
    {
        public Guid Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Apartment { get; init; }
        public string? POBox { get; init; }
        public string? Line2 { get; init; }
        public string? Country { get; set; }

    }
}
