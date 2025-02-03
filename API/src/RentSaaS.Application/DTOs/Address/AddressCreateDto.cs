using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Application.DTOs.Address;

public class AddressCreateDto:BaseEntityDto
{
    [Column(TypeName = "nvarchar(100)")]
    public string? Street { get; set; }
   
    [Column(TypeName = "nvarchar(100)")]
    public string? City { get; set; }
   
    [Column(TypeName = "nvarchar(100)")]
    public string? State { get; set; }

    [Column(TypeName = "nvarchar(15)")]
    public string? PostalCode { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Apartment { get; init; }

    [Column(TypeName = "nvarchar(15)")]
    public string? POBox { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Line2 { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Country { get; set; }
}
