using Microsoft.AspNetCore.Http;

namespace RentSaaS.Application.DTOs.Maintenace
{
    public class MaintenanceCreateDTO
    {
        public Guid? PropertyId { get; set; }
        public string? IssueType { get; set; }
        public string? Priority { get; set; }
        public string? Details { get; set; }
        public DateTime DueDate { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
