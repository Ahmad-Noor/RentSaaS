using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Common;

public abstract class BaseEntity : IEntity
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; } 

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}