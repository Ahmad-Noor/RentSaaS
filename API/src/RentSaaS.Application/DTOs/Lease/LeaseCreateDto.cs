using RentSaaS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Lease
{
    public class LeaseCreateDto : BaseEntityDto
    { 
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Must Choose Property")]
        public Guid PropertyId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal RentAmount { get; set; }
        public string? TenantName { get; set; }
        public string? LeaseType { get; set; }

        public OrganizationDto? Organization { get; set; }
        public PropertyDto? Property { get; set; }


    }
}
