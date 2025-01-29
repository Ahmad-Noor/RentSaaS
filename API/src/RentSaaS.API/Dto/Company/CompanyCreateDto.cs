using System.ComponentModel.DataAnnotations;
using RentSaaS.Domain.Enums;

namespace RentSaaS.API.Dto.Company
{
    public class CompanyCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "OrganizationId is required.")]
        public Guid OrganizationId { get; set; }
        public CompanyType? Type { get; set; }
        public long? Ein { get; set; }
    }
}
