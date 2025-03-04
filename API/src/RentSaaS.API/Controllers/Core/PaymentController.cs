using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentSaaS.API.APIResponse;
using RentSaaS.API.Extensions;
using RentSaaS.API.Models;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Application.DTOs.RecordPayment;
using RentSaaS.Application.Services;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.Controllers.Core;

public class PaymentController :BaseControllery
{

    private readonly ILogger<PaymentController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly FileUploadSettings _fileUploadSettings;
    private readonly IOrganizationService _organizationService;

    public PaymentController(
        ILogger<PaymentController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileManagmentService fileManagementService,
        IOptions<FileUploadSettings> fileUploadSettings, IOrganizationService organizationService) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileManagementService = fileManagementService ?? throw new ArgumentNullException(nameof(fileManagementService));
        _fileUploadSettings = fileUploadSettings.Value ?? throw new ArgumentNullException(nameof(fileUploadSettings));
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));

    }


    // Implementation here

   



    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<GetRecordPaymentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.RecordPaymentRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<GetRecordPaymentDto>>(items);

            return Ok(new APIResponse<List<GetRecordPaymentDto>>(mappedItems, "Payments retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all Payments");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));

        }
    }



    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<RecordPaymentByIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromForm] RecordPaymentCreateDto recordPaymentCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid expense data"));
            }

            var payment = _mapper.Map<RecordPayment>(recordPaymentCreateDto);
            await _unitOfWork.RecordPaymentRepository.AddAsync(payment);

            if (recordPaymentCreateDto.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(payment.Id, recordPaymentCreateDto.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            await _unitOfWork.SaveChangesAsync();

            var createdPayment = _mapper.Map<GetRecordPaymentDto>(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id },
                new APIResponse<GetRecordPaymentDto>(createdPayment, "payment created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating payment");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<RecordPaymentByIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            // Retrieve the expense
            var Recordpayment = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (Recordpayment == null)
            {
                return NotFound(new APIErrorResponse(404, $"Recordpayment with ID {id} not found"));
            }

            // Retrieve the associated files
            var RecordpaymentFiles = await _unitOfWork.RecordPaymentFileRepository.FindAsync(f => f.RecordPaymentId == id);

            // Get the base URL for files
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            var organization = _organizationService.GetCurrentOrganization();

            // Map the expense and files to the DTO
            var mappedRecordPayment = _mapper.Map<RecordPaymentByIdDto>(Recordpayment);
            mappedRecordPayment.Files = RecordpaymentFiles.Select(f => new PaymentfileDto
            {
                Id = f.Id,
                FileName = Path.GetFileName(f.FileName),
                FileSize = f.FileSize,
                UploadedAt = f.UploadedAt,
                Url = $"{Request.Scheme}://{Request.Host.Value}/{f.FileName}"
            }).ToList();

            return Ok(mappedRecordPayment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving payment with ID: {PaymentId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }



    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(APIResponse<RecordPaymentUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] RecordPaymentUpdateDto recordPaymentUpdate)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingPayment = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
            {
                return NotFound(new APIErrorResponse(404, $"Payment with ID {id} not found"));
            }

            // Map updated fields to the existing expense
            _mapper.Map(recordPaymentUpdate, existingPayment);

            // Handle file deletions
            if (recordPaymentUpdate.FilesToDelete?.Any() == true)
            {
                // Update the line causing the error
                var filesToDelete = await _unitOfWork.RecordPaymentFileRepository.FindAsync(f => recordPaymentUpdate.FilesToDelete.Contains(f.Id.ToString()) && f.Id == id);

                if (filesToDelete.Any())
                {
                    foreach (var file in filesToDelete)
                    {
                        _fileManagementService.DeleteFile(file.FileName); // Delete the file from storage
                    }

                    _unitOfWork.RecordPaymentFileRepository.RemoveRange(filesToDelete); // Remove file records from the database
                }
            }

            // Handle new file uploads
            if (recordPaymentUpdate.Files?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(id, recordPaymentUpdate.Files);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            // Update the expense in the database
            await _unitOfWork.RecordPaymentRepository.UpdateAsync(existingPayment);
            await _unitOfWork.SaveChangesAsync();

            var updatedRecordPayment = _mapper.Map<GetRecordPaymentDto>(existingPayment);
            return Ok(new APIResponse<GetRecordPaymentDto>(updatedRecordPayment, "Payment updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating payment with ID: {ExpenseId}", id);
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
            var recordpayment = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (recordpayment == null)
            {
                return NotFound(new APIErrorResponse(404, $"RecordPayment with ID {id} not found"));
            }

            recordpayment.IsDeleted = true;
            recordpayment.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"recordpayment successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting recordpayment with ID: {RecordPaymentId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }









    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid recordpaymentId, IFormFileCollection files)
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

            var source = Path.Combine("Organizations", _organizationService.GetCurrentOrganization().OrganizationId.ToString(), "Payments", recordpaymentId.ToString());
            var filePaths = await _fileManagementService.AddFileAsync(files, source);

            var paymentFiles = filePaths.Select(filePath => new RecordPaymentFile
            {
                RecordPaymentId = recordpaymentId,
                FileName = filePath,
                UploadedAt = DateTime.UtcNow,
                FileSize = files.FirstOrDefault(f => Path.GetFileName(filePath) == f.FileName)?.Length ?? 0
            }).ToList();

            await _unitOfWork.RecordPaymentFileRepository.AddRangeAsync(paymentFiles.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for expense: {RecordPaymentId}", recordpaymentId);
            return (false, "Failed to upload files");
        }
    }

}
