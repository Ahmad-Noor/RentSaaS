
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Address;

public class AddressUpdateDto : AddressCreateDto
{
    public Guid Id { get; set; }

}
