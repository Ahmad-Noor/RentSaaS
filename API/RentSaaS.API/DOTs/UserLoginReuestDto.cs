namespace RentSaaS.API.DOTs;
public class UserLoginReuestDto
{
    public required string Email { get; set; }= string.Empty;
    public required string Password { get; set; } = string.Empty;   
}
