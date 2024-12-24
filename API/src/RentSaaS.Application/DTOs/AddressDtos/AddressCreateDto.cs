using System.ComponentModel.DataAnnotations;

namespace RentSaaS.API.DTOs.AddressDtos;

public class AddressCreateDto
{
    [Required]
    [StringLength(100)]
    public string Street { get; set; }

    [Required]
    [StringLength(50)]
    public string City { get; set; }

    [Required]
    [StringLength(50)]
    public string State { get; set; }

    [Required]
    [StringLength(20)]
    public string PostalCode { get; set; }

    [Required]
    [StringLength(50)]
    public string Country { get; set; }
}
