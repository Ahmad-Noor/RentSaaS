using System.ComponentModel.DataAnnotations;
namespace RentSaaS.API.DOTs;
public class AuthenticateRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}