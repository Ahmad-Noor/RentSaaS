namespace RentSaaS.Common
{
    public class Tenant
    {
        public  required string TenantId { get; set; }
        public  required string Name { get; set; }
        public  string? ConnectionString { get; set; }
    }
}
