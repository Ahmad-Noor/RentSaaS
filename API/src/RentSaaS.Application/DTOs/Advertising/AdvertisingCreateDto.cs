using RentSaaS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingCreateDto : BaseEntityDto
    { 
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Must Choose Property")]
        public Guid PropertyId { get; set; }
        [Required]
        public string Platform { get; set; } = null!;
        public int Views { get; set; }
        public int Leads { get; set; }
        public AdvertizeOrganizDto? Organization { get; set; }
        public AdvertizePropDto? Property { get; set; }


    }
}
