using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace RentSaaS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;   
    private readonly IUnitOfWork _unitOfWork;

    public AddressController(ILogger<AddressController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;  
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _unitOfWork.AddressRepository.GetAll();
        if (countries == null)
        {
            return NotFound();
        }
        return Ok(countries);
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var address = await _unitOfWork.AddressRepository.GetById(id);
        if (address != null)
        {
            return Ok(address);
        }
        return NotFound();
    }
     
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]Address address)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }   

        try
        {
            _logger.LogInformation("Create new address, address street #{AddresStreet}", address.Street);
            _unitOfWork.AddressRepository.Add(address); 
            await _unitOfWork.CompleteAsync();

            return Ok(address);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new address, address street #{AddresStreet}", address.Street); ;
            return new JsonResult($"error on creating new address {address.Street}") { StatusCode = 500 };
        }
    }
       
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Address address)
    {
        if (id != address.Id)
        {
            return BadRequest();
        }

        _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var address = await _unitOfWork.AddressRepository.FirstOrDefaultAsync(w => w.Id == id );
        if (address != null)
        {
            address.IsDeleted = true;
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        return NotFound(id);
    }
}
