using Microsoft.AspNetCore.Identity;
using RentSaaS.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Domain.Entities;
public class User : IdentityUser<long>
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

    public  string FirstName { get; set; }
    public  string LastName { get; set; } 
    public bool? ShowFullName { get; set; }  
    public string? ProfilePicture { get; set; }
    public DateTime? ProfilePictureUpdated { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? PasswordLastChanged { get; set; } 
    public bool? IsActive { get; set; } = true;  
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
     
}
