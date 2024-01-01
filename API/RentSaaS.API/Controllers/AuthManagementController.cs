using Common;
using System.Text;
using Common.Services;
using RentSaaS.Common;
using RentSaaS.API.DOTs;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RentSaaS.API.Configurations;
using Microsoft.Extensions.Options;
namespace RentSaaS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthManagementController : ControllerBase
{
    private readonly ILogger<UserController> _logger; // ILogger takes the type of the class as a parameter
    private readonly ConfigurationDBContext _identityDBContext;
    private readonly IConfiguration _configuration;
     
    private readonly JwtConfig _jwtConfig;

    public AuthManagementController(ILogger<UserController> logger,
                                    IConfiguration configuration,
                                    ConfigurationDBContext identityDB, 
                                    IOptionsMonitor<JwtConfig> jwtConfig)
    {
        _logger = logger;
        _configuration = configuration;
        _identityDBContext = identityDB; 
        _jwtConfig = jwtConfig.CurrentValue;
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
            Request.Password = Password.HashPassword(Request.Password);

            var user = new User
            {
                FirstName = Request.FirstName,
                LastName = Request.LastName,
                ShowFullName = true,
                Email = Request.Email,
                PasswordHash = Password.HashPassword(Request.Password),
                IsActive = true, 
            };
            _identityDBContext.Users.Add(user);
            await _identityDBContext.SaveChangesAsync();

            var token = CreateJwtToken(user);
            return Ok(new AuthenticateResponse(user, token));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new user, Email #{Email}", Request.Email);
            return new JsonResult($"error on creating new user, Email: {Request.Email}") { StatusCode = 500 };
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login ([FromBody]UserLoginReuestDto Request)
    {
        if (!ModelState.IsValid) { return BadRequest("Enter Email & Password."); }

        var user = await _identityDBContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == Request.Email.ToLower());
        if (user == null || !Password.VerifyHashedPassword(user.PasswordHash, Request.Password))
        {
            return BadRequest(new { message = "Username or Password is Incorrect, Please try again." });
        }
        var token = CreateJwtToken(user);
        return Ok(new AuthenticateResponse(user, token));
    }


    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        //var user = await _unitOfWork.UserRepository.GetUser(id.ToString());
        //if (user != null)
        //{
        //    return Ok(user);
        //}
        return NotFound();
    }


    private string CreateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>(key: "JwtConfig:SecretKey"));
        var jwrTokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.GivenName, $"{user.FirstName} {user.LastName}"),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        }),
            //NotBefore = DateTime.Now.AddMinutes(-5),
            Expires = DateTime.UtcNow.AddSeconds(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };

        var token = jwrTokenHandler.CreateToken(tokenDescriptor);
        return jwrTokenHandler.WriteToken(token);
    }

    private async Task<bool> CheckUserNameExistAsync(string userName)
    {
        //var options = new DbContextOptions<RentSaaSDBContext>();
        //options.UseSqlServer(tenantService.GetConnectionString());

        //var dbContext = new RentSaaSDBContext(options, tenantService);
        //var query = dbContext.Users.Where(u => u.UserName == userName);
        var user = await _identityDBContext.Users.SingleOrDefaultAsync(w => w.UserName == userName);
        return user != null;
    }
    private async Task<bool> CheckUserNameEmailAsync(string email)
    {
        var user = await _identityDBContext.Users.SingleOrDefaultAsync(w => w.Email == email);
        return user != null;
    }



    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _identityDBContext.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var user = await _identityDBContext.Users.FirstOrDefaultAsync(w => w.Id == id);
        if (user != null)
        {
            user.IsActive = false;
            user.IsDeleted = true;
            await _identityDBContext.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(id);
    }
}
