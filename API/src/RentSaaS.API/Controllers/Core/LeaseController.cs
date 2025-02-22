using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Lease;
using Microsoft.AspNetCore.Authorization;

namespace RentSaaS.API.Controllers.Core;

public class LeaseController : BaseControllery
{
 
    private readonly ILogger<LeaseController> _logger;

    public LeaseController(ILogger<LeaseController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<LeaseGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var leases = await _unitOfWork.LeaseRepository.GetAllAsync();
        if (leases == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var LeaseMapper = _mapper.Map<List<LeaseGetDto>>(leases);
        return Ok(new APIResponse<List<LeaseGetDto>>(LeaseMapper, "All Data For Lease")); ;
    }


    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<LeaseGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var leases = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
        var LeaseMapper = _mapper.Map<LeaseGetDto>(leases);
        if (leases != null)
        {
            return Ok(new APIResponse<LeaseGetDto>(LeaseMapper, "All Data For Lease"));
        }
        return NotFound(new APIErrorResponse(404));
    }

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
            await _unitOfWork.LeaseRepository.AddAsync(Lease);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<LeaseCreateDto>(leasesDto, "Lease Is Create Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new leases ");
            return new JsonResult($"error on creating new leases") { StatusCode = 500 };
        }
    }


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
}
