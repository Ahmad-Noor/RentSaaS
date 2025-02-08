using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.RentApplication
{
    public class ApplicationOrganizDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public List<string> ApplicationAndLeads { get; set; } = new List<string>();

    }
}
