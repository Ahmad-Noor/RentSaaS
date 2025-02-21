using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Property;
namespace RentSaaS.API.Controllers.Core;

public class PropertyController : BaseControllery
{ 
    private readonly ILogger<PropertyController> _logger;

    public PropertyController(ILogger<PropertyController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger;
    }
     
    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(APIResponse<PropertyCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] PropertyCreateDto propertyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
         
        try
        {
            var property = _mapper.Map<Property>(propertyDto);

            property.CreatedBy = Guid.Parse(User.FindFirst("id")?.Value);

            await _unitOfWork.PropertyRepository.Add(property);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<PropertyCreateDto>(true, "Property Is Create Success", propertyDto));
        }
        catch (Exception ex)
        {
            return new JsonResult($"error on creating new property {propertyDto.Address}") { StatusCode = 500 };
        }
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(APIResponse<List<PropertyGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var properties = await _unitOfWork.PropertyRepository.GetAll();
            if (properties == null)
            {
                return NotFound(new APIErrorResponse(404));
            }

            var PropertyMapper = _mapper.Map<List<PropertyGetDto>>(properties);
            return Ok(new APIResponse<List<PropertyGetDto>>(true, "All Data For Property", PropertyMapper));
        }
        catch (Exception ex)
        {

            return new JsonResult($"error on getting all properties") { StatusCode = 500 };
        }
    }
      
    [HttpGet]
    [Route("GetById/{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var property = await _unitOfWork.PropertyRepository.GetById(id);
        var PropertyMapper = _mapper.Map<PropertyGetDto>(property);
        if (property != null)
        {
            return Ok(new APIResponse<PropertyGetDto>(true, "All Data For Property", PropertyMapper));
        }
        return NotFound();
    }
      
    [HttpPut]
    [Route("Update/{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Update(Guid id, PropertyUpdateDto propertyDto)
    {
        if (id != propertyDto.Id)
        {
            return BadRequest();
        }
        var Property = await _unitOfWork.PropertyRepository.GetById(id);
        _mapper.Map(propertyDto, Property);
        Property.LastModifiedBy = Guid.Parse(User.FindFirst("id")?.Value);
        await _unitOfWork.PropertyRepository.Update(Property);

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
      
    [HttpDelete]
    [Route("Delete/{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var property = await _unitOfWork.PropertyRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (property != null)
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            property.IsDeleted = true;
            property.DeletedBy = Guid.Parse(userIdClaim);
            property.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));

    }
     
}
