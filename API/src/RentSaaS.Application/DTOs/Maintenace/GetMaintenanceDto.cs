namespace RentSaaS.Application.DTOs.Maintenace;

public class GetMaintenanceDto
{
    public Guid Id { get; set; }
    public Guid? PropertyId { get; set; }
    public string? IssueType { get; set; }
    public string? Priority { get; set; }
    public string? Description { get; set; }
}
