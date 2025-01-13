using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RentSaaS.Domain.Entities;

namespace RentSaaS.Application.DTOs.UserDtos;
public class UserRegistrationRequestDto
{
    [Required(ErrorMessage ="Must Enter the First Name")]
    public required string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Must Enter the Last Name")]

    public required string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Must Enter the Email"),EmailAddress(ErrorMessage ="Must Email Valid")]
    public required string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Must Enter the Password")]

    public required string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Must Choose User Type")]


    public required UserType UserType { get; set; } = UserType.Landlord; // Convert to Enum (Landlord -tenant - landlord&&tenant) Enum 


    //public required Guid OrganizationId { get; set; }


}