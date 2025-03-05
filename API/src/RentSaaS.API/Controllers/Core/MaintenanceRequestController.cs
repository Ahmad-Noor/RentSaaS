using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Models;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using Microsoft.Extensions.Options;
using RentSaaS.Application.Services;
using RentSaaS.Application.DTOs.Maintenace;

namespace RentSaaS.API.Controllers.Core;

public class MaintenanceRequestController : BaseControllery
{
    private readonly ILogger<MaintenanceRequestController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly IOrganizationService _organizationService;
    private readonly FileUploadSettings _fileUploadSettings;

    public MaintenanceRequestController(
        ILogger<MaintenanceRequestController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileManagmentService fileManagementService,
        IOptions<FileUploadSettings> fileUploadSettings,
        IOrganizationService organizationService) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileManagementService = fileManagementService ?? throw new ArgumentNullException(nameof(fileManagementService));
        _fileUploadSettings = fileUploadSettings.Value ?? throw new ArgumentNullException(nameof(fileUploadSettings));
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<GetMaintenanceDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.MaintenanceRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<GetMaintenanceDto>>(items);

            return Ok(new APIResponse<List<GetMaintenanceDto>>(mappedItems, "Maintenance retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all maintenance");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<GetMaintenaceByIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            // Retrieve the expense
            var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (maintenance == null)
            {
                return NotFound(new APIErrorResponse(404, $"Maintenance with ID {id} not found"));
            }

            // Retrieve the associated files
            var maintenacePhotos = await _unitOfWork.MaintenancePhotoRepository.FindAsync(f => f.MaintenanceId == id);

            // Get the base URL for files
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            var organization = _organizationService.GetCurrentOrganization();

            // Map the expense and files to the DTO
            var mappedMaintenance = _mapper.Map<GetMaintenaceByIdDto>(maintenance);
            mappedMaintenance.Files = maintenacePhotos.Select(f => new MaintenancePhotoDto
            {
                Id = f.Id,
                FileName = Path.GetFileName(f.FileName),
                FileSize = f.FileSize,
                UploadedAt = f.UploadedAt,
                Url = $"{Request.Scheme}://{Request.Host.Value}/{f.FileName}"
            }).ToList();

            return Ok(mappedMaintenance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expense with ID: {MaintenanceId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<GetMaintenaceByIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromForm] MaintenanceCreateDTO maintenanceCreateDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid maintenance data"));
            }

            var maintenance = _mapper.Map<Maintenance>(maintenanceCreateDTO);
            await _unitOfWork.MaintenanceRepository.AddAsync(maintenance);

            if (maintenanceCreateDTO.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(maintenance.Id, maintenanceCreateDTO.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            await _unitOfWork.SaveChangesAsync();

            var createdMaintenance = _mapper.Map<GetMaintenanceDto>(maintenance);
            return CreatedAtAction(nameof(GetById), new { id = maintenance.Id },
                new APIResponse<GetMaintenanceDto>(createdMaintenance, "Maintenance created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating maintenance");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<MaintenanceUpdateDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] MaintenanceUpdateDTO maintenanceUpdateDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingMaintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (existingMaintenance == null)
            {
                return NotFound(new APIErrorResponse(404, $"Maintenance with ID {id} not found"));
            }

            // Map updated fields to the existing expense
            _mapper.Map(maintenanceUpdateDTO, existingMaintenance);

            // Handle file deletions
            if (maintenanceUpdateDTO.FilesToDelete?.Any() == true)
            {
                // Update the line causing the error
                var photosToDelete = await _unitOfWork.MaintenancePhotoRepository.FindAsync(f => maintenanceUpdateDTO.FilesToDelete.Contains(f.Id.ToString()) && f.MaintenanceId == id);

                if (photosToDelete.Any())
                {
                    foreach (var photo in photosToDelete)
                    {
                        _fileManagementService.DeleteFile(photo.FileName); // Delete the file from storage
                    }

                    _unitOfWork.MaintenancePhotoRepository.RemoveRange(photosToDelete); // Remove file records from the database
                }
            }

            // Handle new file uploads
            if (maintenanceUpdateDTO.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(id, maintenanceUpdateDTO.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            // Update the expense in the database
            await _unitOfWork.MaintenanceRepository.UpdateAsync(existingMaintenance);
            await _unitOfWork.SaveChangesAsync();

            var updatedMaintenance = _mapper.Map<GetMaintenanceDto>(existingMaintenance);
            return Ok(new APIResponse<GetMaintenanceDto>(updatedMaintenance, "Maintenance updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating maintenance with ID: {MaintenanceId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var expense = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new APIErrorResponse(404, $"Maintenance with ID {id} not found"));
            }

            expense.IsDeleted = true;
            expense.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Maintenance successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting maintenance with ID: {MaintenanceId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid maintenanceId, IFormFileCollection files )
    {
        try
        {
            if (files.Count > _fileUploadSettings.MaxFileUploadLimit)
            {
                return (false, $"Maximum {_fileUploadSettings.MaxFileUploadLimit} files can be uploaded");
            }

            foreach (var file in files)
            {
                if (file.Length > _fileUploadSettings.MaxFileSize)
                {
                    return (false, $"file {file.FileName} exceeds maximum size of {_fileUploadSettings.MaxFileSize / 1024 / 1024}MB");
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_fileUploadSettings.AllowedFileTypes.Contains(extension))
                {
                    return (false, $"file type {extension} is not allowed");
                }
            }

            var source = Path.Combine("Organizations", _organizationService.GetCurrentOrganization().OrganizationId.ToString(), "Maintenance", maintenanceId.ToString());
            var photoPaths = await _fileManagementService.AddFileAsync(files, source);

            var maintenancePhotos = photoPaths.Select(photoPath => new MaintenancePhoto
            {
                MaintenanceId = maintenanceId,
                FileName = photoPath,
                UploadedAt = DateTime.UtcNow,
                FileSize = files.FirstOrDefault(f => Path.GetFileName(photoPath) == f.FileName)?.Length ?? 0
            }).ToList();


            await _unitOfWork.MaintenancePhotoRepository.AddRangeAsync(maintenancePhotos.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for maintenance: {MaintenanceId}", maintenanceId);
            return (false, "Failed to upload photos");
        }
    }
}