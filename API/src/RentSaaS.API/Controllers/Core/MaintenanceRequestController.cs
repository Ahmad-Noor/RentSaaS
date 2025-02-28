using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Models;
using RentSaaS.API.Controllers;
using Microsoft.Extensions.Options;
using RentSaaS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.API.Extensions;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Application.DTOs.Maintenace;
using RentSaaS.Domain.Entities;

public class MaintenanceRequestController : BaseControllery
{
    private readonly ILogger<MaintenanceRequestController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly FileUploadSettings _fileUploadSettings;

    public MaintenanceRequestController(
        ILogger<MaintenanceRequestController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileManagmentService fileManagementService,
        IOptions<FileUploadSettings> fileUploadSettings) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileManagementService = fileManagementService ?? throw new ArgumentNullException(nameof(fileManagementService));
        _fileUploadSettings = fileUploadSettings.Value ?? throw new ArgumentNullException(nameof(fileUploadSettings));
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
            var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (maintenance == null)
            {
                return NotFound(new APIErrorResponse(404, $"Maintenance with ID {id} not found"));
            }

            var mappedMaintenance = _mapper.Map<GetMaintenaceByIdDto>(maintenance);
            return Ok(new APIResponse<GetMaintenaceByIdDto>(mappedMaintenance, "Maintenance retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving maintenance with ID: {MaintenanceId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<MaintenanceCreateDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] MaintenanceCreateDTO maintenanceCreateDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid maintenance data"));
            }

            var maintenance = _mapper.Map<Maintenance>(maintenanceCreateDTO);
            await _unitOfWork.MaintenanceRepository.AddAsync(maintenance);

            if (maintenanceCreateDTO.Photo?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(maintenance.Id, maintenance.OrganizationId, maintenanceCreateDTO.Photo);
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
    [ProducesResponseType(typeof(APIResponse<MaintenanceUpdateDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MaintenanceUpdateDTO maintenanceUpdateDTO)
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

            _mapper.Map(maintenanceUpdateDTO, existingMaintenance);
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
            var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (maintenance == null)
            {
                return NotFound(new APIErrorResponse(404, $"Maintenance with ID {id} not found"));
            }

            maintenance.IsDeleted = true;
            maintenance.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Maintenance successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expense with ID: {MaintenanceId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid maintenanceId, Guid organizationId, IFormFileCollection files)
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
                    return (false, $"File {file.FileName} exceeds maximum size of {_fileUploadSettings.MaxFileSize / 1024 / 1024}MB");
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_fileUploadSettings.AllowedFileTypes.Contains(extension))
                {
                    return (false, $"File type {extension} is not allowed");
                }
            }

            var source = Path.Combine(organizationId.ToString(), "Maintenance", maintenanceId.ToString());
            var filePaths = await _fileManagementService.AddFileAsync(files, source);

            var maintenancephoto = filePaths.Select(filePath => new MaintenancePhoto
            {
                MaintenanceId = maintenanceId,
                PhotoName = filePath,
                UploadedAt = DateTime.UtcNow,
                PhotoSize = files.FirstOrDefault(f => Path.GetFileName(filePath) == f.FileName)?.Length ?? 0
            }).ToList();

            await _unitOfWork.MaintenancePhotoRepository.AddRangeAsync(maintenancephoto.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for expense: {MaintenanceId}", maintenanceId);
            return (false, "Failed to upload files");
        }
    }
}