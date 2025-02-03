namespace RentSaaS.Application.DTOs.Lease
{
    public class PropertyDto :BaseEntityDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string? Unite { get; set; }
        public List<string> Leases { get; set; } = new List<string>();
    }
}
