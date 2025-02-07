using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertizeOrganizDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public List<string> Advertising { get; set; } = new List<string>();

    }
}
