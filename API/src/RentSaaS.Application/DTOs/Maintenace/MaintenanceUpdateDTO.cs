using Microsoft.AspNetCore.Http;
using RentSaaS.Application.DTOs.MaintenancePhotoDto;

namespace RentSaaS.Application.DTOs.Maintenace
{
    public class MaintenanceUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string? IssueType { get; set; }
        public string? Priority { get; set; }
        public string? Details { get; set; }
        public DateTime DueDate { get; set; }
        public IFormFileCollection? Files { get; set; }
        public List<string>? FilesToDelete { get; set; }
    }
}
