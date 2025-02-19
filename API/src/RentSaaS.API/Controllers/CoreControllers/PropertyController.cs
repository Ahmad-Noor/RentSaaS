using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.Property;
using System.Security.Claims;
namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PropertyController : ControllerBase
{
    // add comment for github
    private readonly ILogger<PropertyController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;

    public PropertyController(ILogger<PropertyController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }



    #region Add Property

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(ApiResponse<PropertyCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Add([FromBody] PropertyCreateDto propertyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Property = _Mapper.Map<Property>(propertyDto);

        try
        {
     
            Property.CreatedBy = Guid.Parse(User.FindFirst("id")?.Value);

            await _unitOfWork.PropertyRepository.Add(Property);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<PropertyCreateDto>(true, "Property Is Create Success", propertyDto));
        }
        catch (Exception ex)
        {
            return new JsonResult($"error on creating new property {Property.Id}") { StatusCode = 500 };
        }
    }

    #endregion















    #region GetAll

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ApiResponse<List<PropertyGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
      
        var properties = await _unitOfWork.PropertyRepository.GetAll();
        if (properties == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }

        var PropertyMapper = _Mapper.Map<List<PropertyGetDto>>(properties);
        return Ok(new ApiResponse<List<PropertyGetDto>>(true, "All Data For Property", PropertyMapper));
    }

    #endregion



    #region Get By Id

    [HttpGet]
    [Route("GetById/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<PropertyGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var property = await _unitOfWork.PropertyRepository.GetById(id);
        var PropertyMapper = _Mapper.Map<PropertyGetDto>(property);
        if (property != null)
        {
            return Ok(new ApiResponse<PropertyGetDto>(true, "All Data For Property", PropertyMapper));
        }
        return NotFound();
    }

    #endregion




    #region Update

    [HttpPut]
    [Route("Update/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<PropertyUpdateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Update(Guid id, PropertyUpdateDto propertyDto)
    {
        if (id != propertyDto.Id)
        {
            return BadRequest();
        }
        var Property=await _unitOfWork.PropertyRepository.GetById(id);
        _Mapper.Map(propertyDto,Property );
        Property.LastModifiedBy = Guid.Parse(User.FindFirst("id")?.Value);
        await _unitOfWork.PropertyRepository.Update(Property);

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    #endregion



    #region DeleteAsync

    [HttpDelete]
    [Route("Delete/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<PropertyCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
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
        return NotFound(new ApiErrorResponses(404));

    }

    #endregion
}
