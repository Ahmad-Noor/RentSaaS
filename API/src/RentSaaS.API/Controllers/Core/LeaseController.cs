using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Lease;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.API.Models;
using RentSaaS.Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace RentSaaS.API.Controllers.Core;

public class LeaseController : BaseControllery
{

    private readonly ILogger<LeaseController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly IOrganizationService _organizationService;
 


    public LeaseController(ILogger<LeaseController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileManagmentService fileManagementService,
        IOptions<FileUploadSettings> fileUploadSettings,
        IOrganizationService organizationService) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileManagementService = fileManagementService ?? throw new ArgumentNullException(nameof(fileManagementService));
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
    }
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<LeaseGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.LeaseRepository.AsQueryable().Where(e => e.IsDeleted != true).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<LeaseGetDto>>(items);

            return Ok(new APIResponse<List<LeaseGetDto>>(mappedItems, "Lease retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all leases");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }


    [Authorize]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<LeaseGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
            if (lease == null)
            {
                return NotFound(new APIErrorResponse(404, $"Lease with ID {id} not found"));
            }

            var mappedLease = _mapper.Map<LeaseGetDto>(lease);
            return Ok(new APIResponse<LeaseGetDto>(mappedLease, "Lease retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving lease with ID: {LeaseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<LeaseCreateDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] LeaseCreateDto leasesDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid lease data"));
            }
            var lease = _mapper.Map<Lease>(leasesDto);

            await _unitOfWork.LeaseRepository.AddAsync(lease);
            await _unitOfWork.SaveChangesAsync();


            var createdLease = _mapper.Map<LeaseGetDto>(lease);
            return CreatedAtAction(nameof(GetById), new { id = lease.Id },
                new APIResponse<LeaseGetDto>(createdLease, "Lease created successfully"));

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error creating lease");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<LeaseUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] LeaseUpdateDto leasesDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingLease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
            if (existingLease == null)
            {
                return NotFound(new APIErrorResponse(404, $"Lease with ID {id} not found"));
            }

            _mapper.Map(leasesDto, existingLease);
            await _unitOfWork.LeaseRepository.UpdateAsync(existingLease);
            await _unitOfWork.SaveChangesAsync();

            var updatedLease = _mapper.Map<LeaseGetDto>(existingLease);
            return Ok(new APIResponse<LeaseGetDto>(updatedLease, "Lease updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating lease with ID: {LeaseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<LeaseCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(id);
            if (lease == null)
            {
                return NotFound(new APIErrorResponse(404, $"Lease with ID {id} not found"));
            }

            lease.IsDeleted = true;
            lease.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Lease successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting lease with ID: {LeaseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
}
