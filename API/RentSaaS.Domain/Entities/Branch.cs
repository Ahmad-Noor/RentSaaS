using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public record Branch : IEntity
{
    [Column(TypeName = "nvarchar(20)")]
    public string? Code { get;   init; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get;   init; }

    [Column(TypeName = "nvarchar(250)")]
    public string? LogoURL { get;   init; }
    public bool? ShowLogo { get;   init; }

    //[ForeignKey("Address")]
    public Guid? AddressId { get; init; }
    //public virtual Address Address { get; init; }

     
    public Guid? ContactId { get;   init; }

    [Column(TypeName = "nvarchar(100)")]
    public string? CommercialNo { get;   init; }

    [Column(TypeName = "nvarchar(250)")]
    public string? SiteURL { get;   init; } 
    public Guid? DefaultCurrencyId { get;   init; }
    //public Guid? DefaultInventoryId { get;   init; }
    //public Guid? DefaultCostCenterId { get;   init; }
    //public Guid? AccPurchasesId { get;   init; }
    //public Guid? AccSuppliersId { get;   init; }
    //public Guid? AccCashId { get;   init; }
    //public Guid? AccPurchasesReturnsId { get;   init; }
    //public Guid? AccSalesTaxonPurchasesId { get;   init; }
    //public Guid? AccDiscountAcquiredId { get;   init; }
    //public Guid? AccSalesId { get;   init; }
    //public Guid? AccInventoryId { get;   init; }
    //public Guid? AccSalesCostId { get;   init; }
    //public int? InventoryAccountingTypes { get;   init; }
    public int? SystemType { get;   init; }
     
}