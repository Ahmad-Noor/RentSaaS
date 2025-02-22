using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.Advertising;

namespace RentSaaS.API.Controllers.Core;

public class AdvertisingController : BaseControllery
{
    
    private readonly ILogger<AdvertisingController> _logger;

    public AdvertisingController(ILogger<AdvertisingController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


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
}
