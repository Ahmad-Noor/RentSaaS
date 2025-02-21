using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Application.DTOs.Lease;

namespace RentSaaS.API.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LeaseController : ControllerBase
{
    // add comment for github
    private readonly ILogger<LeaseController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LeaseController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = Mapper;
    }

    #region Get All

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<LeaseGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var leases = await _unitOfWork.LeaseRepository.GetAll();
        if (leases == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var LeaseMapper = _mapper.Map<List<LeaseGetDto>>(leases);
        return Ok(new APIResponse<List<LeaseGetDto>>(true, "All Data For Lease", LeaseMapper)); ;
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<LeaseGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.GetById(id);
        var LeaseMapper = _mapper.Map<LeaseGetDto>(leases);
        if (leases != null)
        {
            return Ok(new APIResponse<LeaseGetDto>(true, "All Data For Lease", LeaseMapper));
        }
        return NotFound(new APIErrorResponse(404));
    }


    #endregion


    #region Create Lease

    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<LeaseCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] LeaseCreateDto leasesDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Lease = _mapper.Map<Lease>(leasesDto);

        try
        {
            _logger.LogInformation("Create new leases");
            await _unitOfWork.LeaseRepository.Add(Lease);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<LeaseCreateDto>(true, "Lease Is Create Success", leasesDto));
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
    [ProducesResponseType(typeof(APIResponse<LeaseUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
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
    [ProducesResponseType(typeof(APIResponse<LeaseCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (leases != null)
        {
            leases.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            //return Ok(new APIResponse<LeaseCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));
    }

    #endregion
}
