using Common;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RentSaaS.Domain.Entities;
using RentSaaS.Infrastructure.Data;
using RentSaaS.Application.DTOs;
using RentSaaS.Application.DTOs.UserDtos;
using AutoMapper;
namespace RentSaaS.API.Controllers.SecurityAndAdministration;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly RentSaaSDBContext _rentSaaSDBContext;
    private readonly IConfiguration _configuration;

    public IMapper _Mapper { get; }

    public AuthController(ILogger<UserController> logger,IMapper Mapper,IConfiguration configuration,RentSaaSDBContext db)
    {
        _logger = logger;
        _Mapper = Mapper;
        _configuration = configuration;
        _rentSaaSDBContext = db;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto Request)
    {
        if (!ModelState.IsValid) { return BadRequest(" Invalid request payload"); }

        if (await CheckUserNameEmailAsync(Request.Email))
        {
            return BadRequest(new { Message = "Email Already Exist!" });
        }
        if (!Regex.IsMatch(Request.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
        {
            return BadRequest(new { Message = "password must contain at least eight characters, at least one number and both lower and uppercase letters and least one special character" });
        }

        try
        {
            _logger.LogInformation("Create new user, Email #{Email}", Request.Email);


            var Organization = new Organization()
            {
                Name = string.Concat(Request.FirstName, Request.LastName),
                IsDeleted=false
            };
            _rentSaaSDBContext.Organizations.Add(Organization);
            _rentSaaSDBContext.SaveChanges();

            #region Make Mapper Between this 

            var User = _Mapper.Map<User>(Request);
            User.OrganizationId = Organization.OrganizationId;
            #endregion
            _rentSaaSDBContext.Users.Add(User);
            await _rentSaaSDBContext.SaveChangesAsync();
            var token = CreateJwtToken(User);
            return Ok(new AuthenticateResponse(User, token));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new user, Email #{Email}", Request.Email);
            return new JsonResult($"error on creating new user, Email: {Request.Email}") { StatusCode = 500 };
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginReuestDto Request)
    {
        if (!ModelState.IsValid) { return BadRequest("Enter Email & Password."); }

        var user = await _rentSaaSDBContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == Request.Email.ToLower());
        if (user == null || !Password.VerifyHashedPassword(user.PasswordHash, Request.Password))
        {
            return BadRequest(new { message = "Username or Password is Incorrect, Please try again." });
        }
        var token = CreateJwtToken(user);
        return Ok(new AuthenticateResponse(user, token));
    }














    private string CreateJwtToken(User user)
    {
        var keyString = _configuration.GetValue<string>("Jwt:Key");
        if (string.IsNullOrEmpty(keyString))
        {
            throw new InvalidOperationException("JWT Key is not configured.");
        }

        var key = Encoding.ASCII.GetBytes(keyString);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, $"{user.FirstName} {user.LastName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private async Task<bool> CheckUserNameEmailAsync(string email)
    {
        var user = await _rentSaaSDBContext.Users.SingleOrDefaultAsync(w => w.Email == email);
        return user != null;
    }

}
