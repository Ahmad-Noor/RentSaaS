using System.ComponentModel.DataAnnotations;

namespace Common.Services
{
    public class Tenant
    {
        [Key]
        public required string TenantId { get; set; }
        public required string Name { get; set; }
        public string? DBProvider { get; set; }
        public string? ConnectionString { get; set; }
        public bool? IsDefault { get; set; } = false;
    }
}
