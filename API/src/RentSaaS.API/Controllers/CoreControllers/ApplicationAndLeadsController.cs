using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.RentApplication;

namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApplicationAndLeadsController : ControllerBase
{
    // add comment for github
    private readonly ILogger<ApplicationAndLeadsController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;

    public ApplicationAndLeadsController(ILogger<ApplicationAndLeadsController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }

    #region Get All

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ApplicationGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.GetAll();
        if (application == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }
        var ApplicationMapper = _Mapper.Map<List<ApplicationGetDto>>(application);
        
        return Ok(new ApiResponse<List<ApplicationGetDto>>(true, "All Data For Application", ApplicationMapper)); ;
 
    
    
    
    
    
    
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<ApplicationGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.GetById(id);
        var ApplicationMapper = _Mapper.Map<ApplicationGetDto>(application);
        if (application != null)
        {
            return Ok(new ApiResponse<ApplicationGetDto>(true, "All Data For Application", ApplicationMapper));
        }
        return NotFound(new ApiErrorResponses(404));
    }


    #endregion


    #region Create Lease

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ApplicationCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Add([FromBody] ApplicationCreateDto applicationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Application = _Mapper.Map<ApplicationAndLeads>(applicationDto);

        try
        {
            _logger.LogInformation("Create new application");
            await _unitOfWork.ApplicationAndLeadsRepository.Add(Application);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<ApplicationCreateDto>(true, "Application Is Create Success", applicationDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new application ");
            return new JsonResult($"error on creating new application") { StatusCode = 500 };
        }
    }

    #endregion


    #region Update

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ApplicationUpdateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 404)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    public async Task<IActionResult> Update(Guid id, ApplicationUpdateDto applicationDto)
    {
        if (id != applicationDto.Id)
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
    [ProducesResponseType(typeof(ApiResponse<ApplicationCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var application = await _unitOfWork.ApplicationAndLeadsRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (application != null)
        {
            application.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            //return Ok(new ApiResponse<LeaseCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new ApiErrorResponses(404));
    }

    #endregion
}
