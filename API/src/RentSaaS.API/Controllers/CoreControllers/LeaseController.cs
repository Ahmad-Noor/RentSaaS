// LeaseController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.API.DTOs;
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LeaseController : BaseApiController
{
    //private readonly ILeaseService _leaseService;

    public LeaseController(
        ILogger<LeaseController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        
        //,
        //ILeaseService leaseService
        
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_leaseService = leaseService;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<LeaseDto>))]
    //public async Task<ActionResult<PaginatedResponse<LeaseDto>>> GetAll(
    //    [FromQuery] LeaseFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var leases = await _leaseService.GetLeasesAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(leases);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving leases");
    //        return StatusCode(500, "An error occurred while retrieving leases");
    //    }
    //}

    //[HttpGet("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaseDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<LeaseDetailDto>> GetById(Guid id)
    //{
    //    try
    //    {
    //        var lease = await _leaseService.GetLeaseByIdAsync(id, CurrentUserId);
    //        if (lease == null)
    //        {
    //            return NotFound($"Lease with ID {id} not found");
    //        }

    //        return Ok(lease);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving lease {LeaseId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the lease");
    //    }
    //}

    //[HttpPost]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaseDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<LeaseDto>> Create([FromBody] LeaseCreateDto createDto)
    //{
    //    try
    //    {
    //        var lease = await _leaseService.CreateLeaseAsync(
    //            CurrentUserId,
    //            createDto);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id = lease.Id },
    //            lease);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating lease");
    //        return StatusCode(500, "An error occurred while creating the lease");
    //    }
    //}

    //[HttpPut("{id:guid}")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Update(
    //    Guid id,
    //    [FromBody] LeaseUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _leaseService.UpdateLeaseAsync(
    //            id,
    //            updateDto,
    //            CurrentUserId);

    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating lease");
    //        return StatusCode(500, "An error occurred while updating the lease");
    //    }
    //}

    //[HttpPost("{id:guid}/renew")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaseDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<LeaseDto>> Renew(
    //    Guid id,
    //    [FromBody] LeaseRenewalDto renewalDto)
    //{
    //    try
    //    {
    //        var renewedLease = await _leaseService.RenewLeaseAsync(
    //            id,
    //            renewalDto,
    //            CurrentUserId);

    //        return Ok(renewedLease);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error renewing lease");
    //        return StatusCode(500, "An error occurred while renewing the lease");
    //    }
    //}

    //[HttpPost("{id:guid}/terminate")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Terminate(
    //    Guid id,
    //    [FromBody] LeaseTerminationDto terminationDto)
    //{
    //    try
    //    {
    //        await _leaseService.TerminateLeaseAsync(
    //            id,
    //            terminationDto,
    //            CurrentUserId);

    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error terminating lease");
    //        return StatusCode(500, "An error occurred while terminating the lease");
    //    }
    //}

    //[HttpGet("{id:guid}/documents")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LeaseDocumentDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<List<LeaseDocumentDto>>> GetDocuments(Guid id)
    //{
    //    try
    //    {
    //        var documents = await _leaseService.GetLeaseDocumentsAsync(id, CurrentUserId);
    //        return Ok(documents);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving lease documents");
    //        return StatusCode(500, "An error occurred while retrieving lease documents");
    //    }
    //}

    //[HttpPost("{id:guid}/documents")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UploadDocument(
    //    Guid id,
    //    [FromForm] LeaseDocumentUploadDto uploadDto)
    //{
    //    try
    //    {
    //        await _leaseService.UploadLeaseDocumentAsync(
    //            id,
    //            uploadDto,
    //            CurrentUserId);

    //        return Ok();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error uploading lease document");
    //        return StatusCode(500, "An error occurred while uploading the document");
    //    }
    //}
}