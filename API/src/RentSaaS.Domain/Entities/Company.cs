using System.ComponentModel.DataAnnotations.Schema;
using RentSaaS.Domain.Common;
using RentSaaS.Domain.Enums;

namespace RentSaaS.Domain.Entities;
public class Company : BaseEntity
{

    public required string Name { get; init; }

    public long? Ein { get; set; }
    public CompanyType? type { get; set; }=null;







    [Column(TypeName = "nvarchar(250)")]
    public string? LogoURL { get; init; }
    public bool? ShowLogo { get; init; }
    public Guid? AddressId { get; init; }

    public Guid? ContactId { get; init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? CommercialNo { get; init; }

    [Column(TypeName = "nvarchar(250)")]
    public string? SiteURL { get; init; }
    public Guid? DefaultCurrencyId { get; init; }

}