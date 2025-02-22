using RentSaaS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Lease
{
    public class LeaseCreateDto : BaseEntityDto
    { 
        [Required(ErrorMessage = "Must Choose Property")]
        public Guid? PropertyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? RentAmount { get; set; }
        public string? TenantName { get; set; }
        public string? LeaseType { get; set; }

        public LeasOrganizDto? Organization { get; set; }
        public LeasPropDto? Property { get; set; }


    }
}
