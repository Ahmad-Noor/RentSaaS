using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Domain.Base;

public record class IEntity
{ 
    [Key]
    public required Guid Id { get; set; }
    public string? TenantId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; } = false;
    public string? Note { get; set; }
      
}
