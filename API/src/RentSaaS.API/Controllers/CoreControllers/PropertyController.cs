using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.Property;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("User Claims: {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
        _logger.LogInformation("Is Authenticated: {IsAuthenticated}", User.Identity?.IsAuthenticated);

        var properties = await _unitOfWork.PropertyRepository.GetAll();
        if (properties == null)
        {
            return NotFound();
        }
        return Ok(properties);
    }

    [HttpGet]
    [Route("GetById/{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var property = await _unitOfWork.PropertyRepository.GetById(id);
        if (property != null)
        {
            return Ok(property);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("Add")]
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
            //Unite = property.Unite
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
     
    [HttpPut]
    [Route("Update/{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, PropertyUpdateDto property)
    {
        if (id != property.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id:Guid}")]
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
