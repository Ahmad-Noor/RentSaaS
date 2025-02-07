using RentSaaS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RentSaaS.Application.DTOs.RentApplication;

namespace RentSaaS.Application.DTOs.RentApplication
{
    public class ApplicationCreateDto : BaseEntityDto
    { 
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Must Choose Property")]
        public Guid PropertyId { get; set; }
        [Required]
        public string ApplicantEmail { get; set; }
        public int PhoneNumber { get; set; }
        public string? Message { get; set; }

        public bool Requestbackgroundcheck { get; set; }
        public bool Requestcreditreport { get; set; }
        public ApplicationOrganizDto? Organization { get; set; }
        public ApplicationPropDto? Property { get; set; }


    }
}
