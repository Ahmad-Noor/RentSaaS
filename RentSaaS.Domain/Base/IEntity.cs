using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Base;

public record class IEntity
{ 
    [Key]
    public required Guid Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string TenantId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; } = "";
      
}
