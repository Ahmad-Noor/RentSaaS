using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.ApplicationAndLeads;
using RentSaaS.API.Extensions;

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
    [ProducesResponseType(typeof(APIResponse<List<ApplicationGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.ApplicationAndLeadsRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<ApplicationGetDto>>(items);

            return Ok(new APIResponse<List<ApplicationGetDto>>(mappedItems, "Application retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all application");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var application = await _unitOfWork.ApplicationAndLeadsRepository.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound(new APIErrorResponse(404, $"Application with ID {id} not found"));
            }

            var mappedApplication = _mapper.Map<ApplicationGetDto>(application);
            return Ok(new APIResponse<ApplicationGetDto>(mappedApplication, "Application retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving application with ID: {ApplicationId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<ApplicationGetDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] ApplicationCreateDto applicationDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid Application data"));
            }
            var application = _mapper.Map<ApplicationAndLeads>(applicationDto);

            await _unitOfWork.ApplicationAndLeadsRepository.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();


            var createdApplication = _mapper.Map<ApplicationGetDto>(application);
            return CreatedAtAction(nameof(GetById), new { id = application.Id },
                new APIResponse<ApplicationGetDto>(createdApplication, "application created successfully"));

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error creating application");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ApplicationUpdateDto applicationDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingApplication = await _unitOfWork.ApplicationAndLeadsRepository.GetByIdAsync(id);
            if (existingApplication == null)
            {
                return NotFound(new APIErrorResponse(404, $"Application with ID {id} not found"));
            }

            _mapper.Map(applicationDto, existingApplication);
            await _unitOfWork.ApplicationAndLeadsRepository.UpdateAsync(existingApplication);
            await _unitOfWork.SaveChangesAsync();

            var updatedApplication = _mapper.Map<ApplicationGetDto>(existingApplication);
            return Ok(new APIResponse<ApplicationGetDto>(updatedApplication, "Application updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating application with ID: {ApplicationId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<ApplicationCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var application = await _unitOfWork.ApplicationAndLeadsRepository.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound(new APIErrorResponse(404, $"Application with ID {id} not found"));
            }

            application.IsDeleted = true;
            application.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Application successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting advertising with ID: {AdvertisingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

}
