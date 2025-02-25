namespace RentSaaS.Application.DTOs.Maintenace
{
    public class GetMaintenaceByIdDto
    {
        public Guid Id { get; set; }
        public Guid? PropertyId { get; set; }
        public string? IssueType { get; set; }
        public string? Priority { get; set; }
        public string? Description { get; set; }
        public List<string>? Photo { get; set; }
    }
}
