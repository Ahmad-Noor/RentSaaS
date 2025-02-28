
namespace RentSaaS.Application.DTOs.Tenant
{
    public class TenantGetByIdDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public Guid? ContactId { get; init; }
        public Guid? AddressId { get; init; }
    }
}
