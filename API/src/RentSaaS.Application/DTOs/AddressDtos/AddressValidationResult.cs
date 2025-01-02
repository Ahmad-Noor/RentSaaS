using RentSaaS.Domain.Entities;

namespace RentSaaS.API.DTOs.AddressDtos;

public class AddressValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public Address? StandardizedAddress { get; set; }
}
