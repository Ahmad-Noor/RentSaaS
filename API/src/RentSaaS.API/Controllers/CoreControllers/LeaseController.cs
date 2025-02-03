using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.Lease;

namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LeaseController : ControllerBase
{
    // add comment for github
    private readonly ILogger<LeaseController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;

    public LeaseController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }

    #region Get All

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<LeaseGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
        var leases = await _unitOfWork.LeaseRepository.GetAll();
        if (leases == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }
        var LeaseMapper = _Mapper.Map<List<LeaseGetDto>>(leases);
        return Ok(new ApiResponse<List<LeaseGetDto>>(true, "All Data For Lease", LeaseMapper)); ;
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<LeaseGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.GetById(id);
        var LeaseMapper = _Mapper.Map<LeaseGetDto>(leases);
        if (leases != null)
        {
            return Ok(new ApiResponse<LeaseGetDto>(true, "All Data For Lease", LeaseMapper));
        }
        return NotFound();
    }


    #endregion


    #region Create Lease

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<LeaseCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Add([FromBody] LeaseCreateDto leasesDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Lease = _Mapper.Map<Lease>(leasesDto);

        try
        {
            _logger.LogInformation("Create new leases");
            await _unitOfWork.LeaseRepository.Add(Lease);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<LeaseCreateDto>(true, "Lease Is Create Success", leasesDto));
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
    [ProducesResponseType(typeof(ApiResponse<LeaseUpdateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 404)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    public async Task<IActionResult> Update(Guid id, LeaseUpdateDto leasesDto)
    {
        if (id != leasesDto.Id)
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
    [ProducesResponseType(typeof(ApiResponse<LeaseCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (leases != null)
        {
            leases.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            //return Ok(new ApiResponse<LeaseCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new ApiErrorResponses(404));
    }

    #endregion
}
