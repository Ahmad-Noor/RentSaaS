using RentSaaS.Application.DTOs.Expense;

namespace RentSaaS.Application.DTOs.Maintenace
{
    public class GetMaintenaceByIdDto
    {
        public Guid Id { get; set; }
        public Guid? PropertyId { get; set; }
        public string? IssueType { get; set; }
        public string? Priority { get; set; }
        public string? Details { get; set; }
        public DateTime DueDate { get; set; }
        public List<MaintenancePhotoDto>? Files { get; set; }
    }
}
