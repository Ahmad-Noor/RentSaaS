using RentSaaS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.Dtos.Company
{
    public class CompanyGetDto
    {

        public string Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "OrganizationId is required.")]
        public Guid OrganizationId { get; set; }
        public CompanyType? Type { get; set; }
        public long? Ein { get; set; }
    }
}
