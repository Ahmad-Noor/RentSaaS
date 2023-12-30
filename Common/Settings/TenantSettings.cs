namespace RentSaaS.Common;
public class TenantSettings
{
    public Configuration? Defaults{ get; set; } = default;
    public List<Tenant> Tenants { get; set; } = [];
}
