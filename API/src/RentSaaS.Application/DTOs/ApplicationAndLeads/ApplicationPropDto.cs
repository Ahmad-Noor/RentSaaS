namespace RentSaaS.Application.DTOs.RentApplication
{
    public class ApplicationPropDto : BaseEntityDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string? Unite { get; set; }
        public List<string> ApplicationAndLeads { get; set; } = new List<string>();

    }
}
