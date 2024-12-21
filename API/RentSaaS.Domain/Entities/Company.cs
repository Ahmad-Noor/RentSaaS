using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public class Company : IEntity
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public long OrganizationId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; }

    public long CompanyId { get; set; } 

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get;   init; }

    [Column(TypeName = "nvarchar(250)")]
    public string? LogoURL { get;   init; }
    public bool? ShowLogo { get;   init; }

    //[ForeignKey("Address")]
    public long? AddressId { get; init; }
    //public virtual Address Address { get; init; }

     
    public long? ContactId { get;   init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? CommercialNo { get;   init; }

    [Column(TypeName = "nvarchar(250)")]
    public string? SiteURL { get;   init; } 
    public long? DefaultCurrencyId { get;   init; }
    //public long? DefaultInventoryId { get;   init; }
    //public long? DefaultCostCenterId { get;   init; }
    //public long? AccPurchasesId { get;   init; }
    //public long? AccSuppliersId { get;   init; }
    //public long? AccCashId { get;   init; }
    //public long? AccPurchasesReturnsId { get;   init; }
    //public long? AccSalesTaxonPurchasesId { get;   init; }
    //public long? AccDiscountAcquiredId { get;   init; }
    //public long? AccSalesId { get;   init; }
    //public long? AccInventoryId { get;   init; }
    //public long? AccSalesCostId { get;   init; }
    //public int? InventoryAccountingTypes { get;   init; }
    public int? SystemType { get;   init; }
     
}