using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RentSaaS.Application.DTOs.Property;
namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


public class PropertyController : ControllerBase
{
    // add comment for github
    private readonly ILogger<PropertyController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PropertyController(ILogger<PropertyController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _unitOfWork.PropertyRepository.GetAll();
        if (countries == null)
        {
            return NotFound();
        }
        return Ok(countries);
    }

    [HttpGet]
    //[Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var property = await _unitOfWork.PropertyRepository.GetById(id);
        if (property != null)
        {
            return Ok(property);
        }
        return NotFound();
    }



    [Authorize]
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] PropertyCreateDto property)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }


        var Property = new Property
        {
            OrganizationId = property.OrganizationId,
            CreatedAt = property.CreatedAt,
            CreatedBy = property.CreatedBy,
            LastModifiedAt = property.LastModifiedAt,
            LastModifiedBy = property.LastModifiedBy,
            IsDeleted = property.IsDeleted,
            DeletedAt = property.DeletedAt,
            DeletedBy = property.DeletedBy,
            Note = property.Note,

            // Mapping property fields
            Address = property.Address,
            Unite = property.Unite
        };




        try
        {
            //_logger.LogInformation("Create new property, property name #{Id}", Property.Id);
            await _unitOfWork.PropertyRepository.Add(Property);
            await _unitOfWork.SaveChangesAsync();

            return Ok(property);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "error on creating new property, property name #{Id}", property.Id);
            return new JsonResult($"error on creating new property {Property.Id}") { StatusCode = 500 };
        }
    }












    //[Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, PropertyUpdateDto property)
    {
        if (id != property.Id)
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
        var property = await _unitOfWork.PropertyRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (property != null)
        {
            property.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(id);

    }
}
