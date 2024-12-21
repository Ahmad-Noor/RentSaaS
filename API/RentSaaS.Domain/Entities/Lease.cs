namespace RentSaaS.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentSaaS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Lease : IEntity
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

    [Key]
    public long LeaseId { get; set; }   

    [Required]
    public long PropertyId { get; set; }   

    [Required]
    public DateTime StartDate { get; set; }  

    [Required]
    public DateTime EndDate { get; set; }  

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal RentAmount { get; set; }  

    public string LeaseTerms { get; set; }  

    // Navigation properties
    public Organization Organization { get; set; }   
    public Property Property { get; set; }  
}