// MaintenanceRequestController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MaintenanceRequestController : BaseApiController
{
    //private readonly IMaintenanceRequestService _maintenanceService;

    public MaintenanceRequestController(
        ILogger<MaintenanceRequestController> logger,
        IUnitOfWork unitOfWork, 
        IMapper mapper
        //,
        //IMaintenanceRequestService maintenanceService
        
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_maintenanceService = maintenanceService;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MaintenanceRequestDto>))]
    //public async Task<ActionResult<PaginatedResponse<MaintenanceRequestDto>>> GetAll(
    //    [FromQuery] MaintenanceRequestFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var requests = await _maintenanceService.GetRequestsAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(requests);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving maintenance requests");
    //        return StatusCode(500, "An error occurred while retrieving maintenance requests");
    //    }
    //}

    //[HttpGet("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaintenanceRequestDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MaintenanceRequestDetailDto>> GetById(Guid id)
    //{
    //    try
    //    {
    //        var request = await _maintenanceService.GetRequestByIdAsync(id, CurrentUserId);
    //        if (request == null)
    //        {
    //            return NotFound($"Maintenance request with ID {id} not found");
    //        }

    //        return Ok(request);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving maintenance request {RequestId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the maintenance request");
    //    }
    //}

    //[HttpPost]
    //[Authorize(Roles = "Tenant")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MaintenanceRequestDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<MaintenanceRequestDto>> Create(
    //    [FromBody] MaintenanceRequestCreateDto createDto)
    //{
    //    try
    //    {
    //        var request = await _maintenanceService.CreateRequestAsync(
    //            CurrentUserId,
    //            createDto);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id = request.Id },
    //            request);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating maintenance request");
    //        return StatusCode(500, "An error occurred while creating the maintenance request");
    //    }
    //}

    //[HttpPut("{id:guid}/status")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateStatus(
    //    Guid id,
    //    [FromBody] MaintenanceRequestStatusUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _maintenanceService.UpdateRequestStatusAsync(
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
    //        _logger.LogError(ex, "Error updating maintenance request status");
    //        return StatusCode(500, "An error occurred while updating the request status");
    //    }
    //}

    //[HttpPost("{id:guid}/comments")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MaintenanceRequestCommentDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MaintenanceRequestCommentDto>> AddComment(
    //    Guid id,
    //    [FromBody] MaintenanceRequestCommentCreateDto commentDto)
    //{
    //    try
    //    {
    //        var comment = await _maintenanceService.AddCommentAsync(
    //            id,
    //            commentDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id },
    //            comment);
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
    //        _logger.LogError(ex, "Error adding comment to maintenance request");
    //        return StatusCode(500, "An error occurred while adding the comment");
    //    }
    //}

    //[HttpPost("{id:guid}/images")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UploadImages(
    //    Guid id,
    //    [FromForm] List<IFormFile> images)
    //{
    //    try
    //    {
    //        await _maintenanceService.AddRequestImagesAsync(
    //            id,
    //            images,
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
    //        _logger.LogError(ex, "Error uploading images for maintenance request");
    //        return StatusCode(500, "An error occurred while uploading images");
    //    }
    //}
}