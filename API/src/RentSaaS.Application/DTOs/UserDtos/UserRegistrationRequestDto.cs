namespace RentSaaS.Application.DTOs.UserDtos;
public class UserRegistrationRequestDto
{
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string UserType { get; set; } = string.Empty;
    public required Guid OrganizationId { get; set; }


}