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
using RentSaaS.API.Helper;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;
using System.Text.Json;
namespace RentSaaS.API.Controllers.SecurityAndAdministration;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly RentSaaSDBContext _rentSaaSDBContext;
    private readonly IConfiguration _configuration;

    public UserManager<User> _userManager { get; }
    public IMapper _Mapper { get; }

    public AuthController(UserManager<User> userManager,ILogger<UserController> logger,IMapper Mapper,IConfiguration configuration,RentSaaSDBContext db)
    {
        _userManager = userManager;
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




    [HttpPost("external-login")]
    public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto model)
    {
        var idToken = await ExchangeAuthorizationCodeForIdToken(model.IdToken);

        var payload = await VerifyGoogleToken(model);
        if (payload == null)
            return BadRequest(new { message = "Invalid External Authentication" });

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new User
            {
                Email = payload.Email,
                UserName = payload.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                IsDeleted = false,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                UserType = "landlord"
            };
            await _userManager.CreateAsync(user);
        }

        var token = CreateJwtToken(user);
        return Ok(new { token });
    }




    private async Task<string> ExchangeAuthorizationCodeForIdToken(string authorizationCode)
    {
        using (var client = new HttpClient())
        {
            var values = new Dictionary<string, string>
        {
            { "code", authorizationCode },
            { "client_id", "YOUR_CLIENT_ID" },
            { "client_secret", "YOUR_CLIENT_SECRET" },
            { "redirect_uri", "YOUR_REDIRECT_URI" },
            { "grant_type", "authorization_code" }
        };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseString);
            var root = jsonDoc.RootElement;

            return root.GetProperty("id_token").GetString();
        }
    }





    private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto model)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { "1000319891618-fnnc0set8ng1rrrke3hujb67cd6cpb5u.apps.googleusercontent.com" }
        };
        return await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
    }













    private string CreateJwtToken(User user)
    {
        var keyString = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
        {
            throw new InvalidOperationException("JWT configuration is missing.");
        }

        var key = Encoding.UTF8.GetBytes(keyString);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, $"{user.FirstName} {user.LastName}"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        // 🔹 Add organization ID if it exists
        if (!string.IsNullOrEmpty(user.OrganizationId.ToString()))
        {
            claims.Add(new Claim("organizationId", user.OrganizationId.ToString()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = issuer,
            Audience = audience,
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
