using Microsoft.AspNetCore.Identity; 
namespace Common.Services;
public class User: IdentityUser<Guid>
{ 
    public required string FirstName { get; set; }
    public required string LastName{ get; set; }  
    public bool? ShowFullName { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime? ProfilePictureUpdated { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? PasswordLastChanged { get; set; } 
    public bool? IsActive { get; set; } = false;
    public bool? IsDeleted { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
     
}
