using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.Advertising;
using Microsoft.Extensions.Options;
using RentSaaS.API.Models;
using RentSaaS.Application.Services;
using RentSaaS.Application.DTOs.Expense;

namespace RentSaaS.API.Controllers.Core;

public class AdvertisingController : BaseControllery
{

    private readonly ILogger<AdvertisingController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly IOrganizationService _organizationService;
    private readonly FileUploadSettings _fileUploadSettings;

    public AdvertisingController(
        ILogger<AdvertisingController> logger,
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
    [ProducesResponseType(typeof(APIResponse<List<AdvertisingGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.AdvertisingRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<AdvertisingGetDto>>(items);

            return Ok(new APIResponse<List<AdvertisingGetDto>>(mappedItems, "Advertizing retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all advertizing");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<GetAdvertizingById>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            // Retrieve the expense
            var advertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (advertising == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertizing with ID {id} not found"));
            }

            // Retrieve the associated files
            var advertizingFiles = await _unitOfWork.AdvertisingFileRepository.FindAsync(f => f.AdvertisingId == id);

            // Get the base URL for files
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            var organization = _organizationService.GetCurrentOrganization();

            // Map the expense and files to the DTO
            var mappedAdvertizing = _mapper.Map<GetAdvertizingById>(advertising);
            mappedAdvertizing.Files = advertizingFiles.Select(f => new AdvertizingFileDTO
            {
              Id = f.Id,
                FileName = Path.GetFileName(f.FileName),
                FileSize = f.FileSize,
                UploadedAt = f.UploadedAt,
                Url = $"{Request.Scheme}://{Request.Host.Value}/{f.FileName}"
            }).ToList();

            return Ok(mappedAdvertizing);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving advertizing with ID: {AdvertizingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<GetAdvertizingById>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromForm] AdvertisingCreateDto advertisingCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid advertizing data"));
            }

            var advertising = _mapper.Map<Advertising>(advertisingCreateDto);
            await _unitOfWork.AdvertisingRepository.AddAsync(advertising);

            if (advertisingCreateDto.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(advertising.Id, advertisingCreateDto.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            await _unitOfWork.SaveChangesAsync();

            var createdAdvertizing = _mapper.Map<AdvertisingGetDto>(advertising);
            return CreatedAtAction(nameof(GetById), new { id = advertising.Id },
                new APIResponse<AdvertisingGetDto>(createdAdvertizing, "Advertizing created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating advertizing");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] AdvertisingUpdateDto advertisingUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingAdvertizing = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (existingAdvertizing == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertizing with ID {id} not found"));
            }

            // Map updated fields to the existing expense
            _mapper.Map(advertisingUpdateDto, existingAdvertizing);

            // Handle file deletions
            if (advertisingUpdateDto.FilesToDelete?.Any() == true)
            {
                // Update the line causing the error
                var filesToDelete = await _unitOfWork.AdvertisingFileRepository.FindAsync(f => advertisingUpdateDto.FilesToDelete.Contains(f.Id.ToString()) && f.AdvertisingId == id);

                if (filesToDelete.Any())
                {
                    foreach (var file in filesToDelete)
                    {
                        _fileManagementService.DeleteFile(file.FileName); // Delete the file from storage
                    }

                    _unitOfWork.AdvertisingFileRepository.RemoveRange(filesToDelete); // Remove file records from the database
                }
            }

            // Handle new file uploads
            if (advertisingUpdateDto.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(id, advertisingUpdateDto.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            // Update the expense in the database
            await _unitOfWork.AdvertisingRepository.UpdateAsync(existingAdvertizing);
            await _unitOfWork.SaveChangesAsync();

            var updatedAdvertizing = _mapper.Map<AdvertisingGetDto>(existingAdvertizing);
            return Ok(new APIResponse<AdvertisingGetDto>(updatedAdvertizing, "Advertizing updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating advertizing with ID: {AdvertizingId}", id);
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
            var advertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (advertising == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertizing with ID {id} not found"));
            }

            advertising.IsDeleted = true;
            advertising.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Advertizing successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting advertizing with ID: {AdvertizingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid advertizingId, IFormFileCollection files)
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

            var source = Path.Combine("Organizations", _organizationService.GetCurrentOrganization().OrganizationId.ToString(), "Advertizing", advertizingId.ToString());
            var filePaths = await _fileManagementService.AddFileAsync(files, source);

            var advertizingFiles = filePaths.Select(filePath => new AdvertisingFile
            {
                AdvertisingId = advertizingId,
                FileName = filePath,
                UploadedAt = DateTime.UtcNow,
                FileSize = files.FirstOrDefault(f => Path.GetFileName(filePath) == f.FileName)?.Length ?? 0
            }).ToList();


            await _unitOfWork.AdvertisingFileRepository.AddRangeAsync(advertizingFiles.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for advertizing: {AdvertizingId}", advertizingId);
            return (false, "Failed to upload files");
        }
    }
}

