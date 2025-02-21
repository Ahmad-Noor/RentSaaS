// RentalApplicationController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.API.DTOs;
using RentSaaS.Domain;

public class RentalApplicationController : BaseApiController
{
    //private readonly IRentalApplicationService _applicationService;

    public RentalApplicationController(
        ILogger<RentalApplicationController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,
        //IRentalApplicationService applicationService
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_applicationService = applicationService;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<RentalApplicationDto>))]
    //public async Task<ActionResult<PaginatedResponse<RentalApplicationDto>>> GetAll(
    //    [FromQuery] RentalApplicationFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var applications = await _applicationService.GetApplicationsAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(applications);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving rental applications");
    //        return StatusCode(500, "An error occurred while retrieving applications");
    //    }
    //}

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RentalApplicationDto))]
    //public async Task<ActionResult<RentalApplicationDto>> Submit(
    //    [FromBody] RentalApplicationCreateDto createDto)
    //{
    //    try
    //    {
    //        var application = await _applicationService.SubmitApplicationAsync(
    //            createDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id = application.Id },
    //            application);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error submitting rental application");
    //        return StatusCode(500, "An error occurred while submitting the application");
    //    }
    //}

    //[HttpPut("{id:guid}/status")]
    //[Authorize(Roles = "Landlord")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public async Task<IActionResult> UpdateStatus(
    //    Guid id,
    //    [FromBody] ApplicationStatusUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _applicationService.UpdateApplicationStatusAsync(
    //            id,
    //            updateDto.Status,
    //            updateDto.Notes,
    //            CurrentUserId);

    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating application status {ApplicationId}", id);
    //        return StatusCode(500, "An error occurred while updating the application status");
    //    }
    //}

    //[HttpPost("{id:guid}/documents")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> UploadDocuments(
    //    Guid id,
    //    [FromForm] List<IFormFile> documents)
    //{
    //    try
    //    {
    //        await _applicationService.AddApplicationDocumentsAsync(
    //            id,
    //            documents,
    //            CurrentUserId);

    //        return Ok();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error uploading documents for application {ApplicationId}", id);
    //        return StatusCode(500, "An error occurred while uploading documents");
    //    }
    //}
}