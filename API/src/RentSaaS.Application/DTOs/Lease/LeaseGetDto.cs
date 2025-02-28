namespace RentSaaS.Application.DTOs.Lease
{
    public class LeaseGetDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal RentAmount { get; set; }
        public string? TenantName { get; set; }
        public string? LeaseType { get; set; }

    }
}
