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
namespace RentSaaS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger; // ILogger takes the type of the class as a parameter
    private readonly IdentityDBContext _identityDBContext;
    private readonly IConfiguration _configuration;

    public UserController(ILogger<UserController> logger, IConfiguration configuration, IdentityDBContext identityDB)
    {
        _logger = logger;
        _configuration = configuration;
        _identityDBContext = identityDB;
    }
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        if (model == null) { return BadRequest(); }

        var user = await _identityDBContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == model.Email.ToLower());
        if (user != null && Password.VerifyHashedPassword(user.PasswordHash, model.Password))
        {
            var token = CreateJwtToken(user);
            return Ok(new AuthenticateResponse(user, token));
        }
        return BadRequest(new { message = "Username or Password is Incorrect, Please try again." });
    }

    //[Authorize]
    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    var countries = await _unitOfWork.UserRepository.GetAll();
    //    if (countries == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(countries);
    //}

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


    [HttpPost]
    public async Task<IActionResult> Add(User user)
    {
        if (user == null)
        {
            return BadRequest();
        }
        if (await CheckUserNameExistAsync(user.UserName))
        {
            return BadRequest(new { Message = "UserName Already Exist!" });
        }
        if (await CheckUserNameEmailAsync(user.Email))
        {
            return BadRequest(new { Message = "Email Already Exist!" });
        }
        if (!Regex.IsMatch(user.PasswordHash, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
        {
            return BadRequest(new { Message = "password must contain at least eight characters, at least one number and both lower and uppercase letters and least one special character" });
        }

        try
        {
            _logger.LogInformation("Create new user, user name #{UserName}", user.UserName);
            user.PasswordHash = Password.HashPassword(user.PasswordHash);
            _identityDBContext.Users.Add(user);
            await _identityDBContext.SaveChangesAsync();

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new user #{UserName}", user.UserName);
            return new JsonResult($"error on creating new user {user.UserName}") { StatusCode = 500 };
        }
    }

    private string CreateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>(key: "SecretKey"));
        var jwrTokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            NotBefore = DateTime.Now.AddMinutes(-5),
            Expires = DateTime.Now.AddSeconds(10),
            SigningCredentials = credentials
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
