using RentSaaS.Application.DTOs.Advertising;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingUpdateDto : AdvertisingCreateDto
    {
        public Guid Id { get; set; }

    }
}
