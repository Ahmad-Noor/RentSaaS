using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities;
public class Lease : BaseEntity
{

    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal RentAmount { get; set; }

    public string? LeaseTerms { get; set; }

    // Navigation properties
    public Organization Organization { get; set; }
    public Property Property { get; set; }
}