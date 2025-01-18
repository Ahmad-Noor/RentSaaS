using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Domain.Entities;
public class User : IdentityUser<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? Note { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? UserType { get; set; }
    public bool? ShowFullName { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime? ProfilePictureUpdated { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? PasswordLastChanged { get; set; }
    public bool? IsActive { get; set; } = true;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }


    public User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
