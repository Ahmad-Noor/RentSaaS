using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.RentApplication;

namespace RentSaaS.API.Controllers.Core;

public class ApplicationAndLeadsController : BaseControllery
{

    private readonly ILogger<ApplicationAndLeadsController> _logger;

    public ApplicationAndLeadsController(ILogger<ApplicationAndLeadsController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<ApplicationGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.GetAllAsync();
        if (application == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var ApplicationMapper = _mapper.Map<List<ApplicationGetDto>>(application);
        
        return Ok(new APIResponse<List<ApplicationGetDto>>(ApplicationMapper, "All Data For Application")); ;
 
    
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.GetByIdAsync(id);
        var ApplicationMapper = _mapper.Map<ApplicationGetDto>(application);
        if (application != null)
        {
            return Ok(new APIResponse<ApplicationGetDto>(ApplicationMapper, "All Data For Application"));
        }
        return NotFound(new APIErrorResponse(404));
    }


    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<ApplicationCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] ApplicationCreateDto applicationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Application = _mapper.Map<ApplicationAndLeads>(applicationDto);

        try
        {
            _logger.LogInformation("Create new application");
            await _unitOfWork.ApplicationAndLeadsRepository.AddAsync(Application);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<ApplicationCreateDto>(applicationDto, "Application Is Create Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new application ");
            return new JsonResult($"error on creating new application") { StatusCode = 500 };
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    public async Task<IActionResult> Update(Guid id, ApplicationUpdateDto applicationDto)
    {
        if (id != applicationDto.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (application != null)
        {
            application.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            //return Ok(new APIResponse<LeaseCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));
    }

}
