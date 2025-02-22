

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdvertisingController : ControllerBase
{
    // add comment for github
    private readonly ILogger<LeaseController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdvertisingController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork;
        _mapper = Mapper;
    }

    #region Get All

    [Authorize]
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(APIResponse<List<AdvertisingGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var advertising = await _unitOfWork.AdvertisingRepository.GetAllAsync();
        if (advertising == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var AdvertisingMapper = _mapper.Map<List<AdvertisingGetDto>>(advertising);
        return Ok(new APIResponse<List<AdvertisingGetDto>>(AdvertisingMapper, "All Data For Advertising")); 
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var advertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
        var AdvertisingMapper = _mapper.Map<AdvertisingGetDto>(advertising);
        if (advertising != null)
        {
            return Ok(new APIResponse<AdvertisingGetDto>(AdvertisingMapper, "All Data For Advertising"));
        }
        return NotFound(new APIErrorResponse(404));
    }


    #endregion


    #region Create Lease

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] AdvertisingCreateDto advertisingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Advertising = _mapper.Map<Advertising>(advertisingDto);

        try
        {
            _logger.LogInformation("Create new Advertising");
            await _unitOfWork.AdvertisingRepository.AddAsync(Advertising);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<AdvertisingCreateDto>( advertisingDto, "Advertising Is Create Success"));
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
    [ProducesResponseType(typeof(APIResponse<AdvertisingUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
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
    [ProducesResponseType(typeof(APIResponse<AdvertisingCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var advertising = await _unitOfWork.AdvertisingRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (advertising != null)
        {
            advertising.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
          //  return Ok(new APIResponse<AdvertisingCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));
    }

    #endregion
}
