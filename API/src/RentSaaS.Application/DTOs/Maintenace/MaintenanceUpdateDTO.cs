using Microsoft.AspNetCore.Http;

namespace RentSaaS.Application.DTOs.Maintenace
{
    public class MaintenanceUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string? IssueType { get; set; }
        public string? Priority { get; set; }
        public string? Description { get; set; }
        public string[]? Photo { get; set; }
        public IFormFile[]? NewPhoto { get; set; }
        public string[]? ExistingPhoto { get; set; }

    }
}
