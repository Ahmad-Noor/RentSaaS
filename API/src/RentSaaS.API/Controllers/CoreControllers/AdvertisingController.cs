

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdvertisingController : ControllerBase
{
    // add comment for github
    private readonly ILogger<LeaseController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;

    public AdvertisingController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }

    #region Get All

    [Authorize]
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(ApiResponse<List<AdvertisingGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
        var advertising = await _unitOfWork.AdvertisingRepository.GetAll();
        if (advertising == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }
        var AdvertisingMapper = _Mapper.Map<List<AdvertisingGetDto>>(advertising);
        return Ok(new ApiResponse<List<AdvertisingGetDto>>(true, "All Data For Advertising",AdvertisingMapper)); 
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<AdvertisingGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var advertising = await _unitOfWork.AdvertisingRepository.GetById(id);
        var AdvertisingMapper = _Mapper.Map<AdvertisingGetDto>(advertising);
        if (advertising != null)
        {
            return Ok(new ApiResponse<AdvertisingGetDto>(true, "All Data For Advertising", AdvertisingMapper));
        }
        return NotFound(new ApiErrorResponses(404));
    }


    #endregion


    #region Create Lease

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(ApiResponse<AdvertisingCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Add([FromBody] AdvertisingCreateDto advertisingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Advertising = _Mapper.Map<Advertising>(advertisingDto);

        try
        {
            _logger.LogInformation("Create new Advertising");
            await _unitOfWork.AdvertisingRepository.Add(Advertising);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<AdvertisingCreateDto>(true, "Advertising Is Create Success",advertisingDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new leases ");
            return new JsonResult($"error on creating new leases") { StatusCode = 500 };
        }
    }

    #endregion


    #region Update

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AdvertisingUpdateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 404)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    public async Task<IActionResult> Update(Guid id, AdvertisingUpdateDto advertisingDto)
    {
        if (id != advertisingDto.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    #endregion


    #region Delete

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AdvertisingCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var advertising = await _unitOfWork.AdvertisingRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (advertising != null)
        {
            advertising.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
          //  return Ok(new ApiResponse<AdvertisingCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new ApiErrorResponses(404));
    }

    #endregion
}
