namespace RentSaaS.API.DOTs;
public class UserRegistrationRequestDto
{
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string TenantId { get; set; } = string.Empty;

}