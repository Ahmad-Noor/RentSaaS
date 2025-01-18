using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LeaseController : ControllerBase
{
    // add comment for github
    private readonly ILogger<LeaseController> _logger;   
    private readonly IUnitOfWork _unitOfWork;

    public LeaseController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;  
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leases = await _unitOfWork.LeaseRepository.GetAll();
        if (leases == null)
        {
            return NotFound();
        }
        return Ok(leases);
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.GetById(id);
        if (leases != null)
        {
            return Ok(leases);
        }
        return NotFound();
    }
     
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Lease leases)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            _logger.LogInformation("Create new leases");
            await _unitOfWork.LeaseRepository.Add(leases); 
            await _unitOfWork.SaveChangesAsync();

            return Ok(leases);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new leases ");
            return new JsonResult($"error on creating new leases") { StatusCode = 500 };
        }
    }
       
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Lease leases)
    {
        if (id != leases.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.FirstOrDefaultAsync(w => w.Id == id );
        if (leases != null)
        {
            leases.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(id);
    }
}
