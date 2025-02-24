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

    public PaymentController(
        ILogger<PaymentController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileManagmentService fileManagementService,
        IOptions<FileUploadSettings> fileUploadSettings) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileManagementService = fileManagementService ?? throw new ArgumentNullException(nameof(fileManagementService));
        _fileUploadSettings = fileUploadSettings.Value ?? throw new ArgumentNullException(nameof(fileUploadSettings));
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
            var query = _unitOfWork.RecordPaymentFileRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<GetRecordPaymentDto>>(items);

            return Ok(new APIResponse<List<GetRecordPaymentDto>>(mappedItems, "Expenses retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all RecordPayment");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<RecordPaymentCreateDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] RecordPaymentCreateDto recordPaymentCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid expense data"));
            }

            var recordPayment = _mapper.Map<RecordPayment>(recordPaymentCreateDto);
            await _unitOfWork.RecordPaymentRepository.AddAsync(recordPayment);

            if (recordPaymentCreateDto.ReceiptsFiles?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(recordPayment.Id, recordPayment.OrganizationId, recordPaymentCreateDto.ReceiptsFiles);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            await _unitOfWork.SaveChangesAsync();

            var createdRecordedpayment = _mapper.Map<GetRecordPaymentDto>(recordPayment);
            return CreatedAtAction(nameof(GetById), new { id = recordPayment.Id },
                new APIResponse<GetRecordPaymentDto>(createdRecordedpayment, "Record payment created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Record Payment");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<GetRecordPaymentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var recordpayment = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (recordpayment == null)
            {
                return NotFound(new APIErrorResponse(404, $"Expense with ID {id} not found"));
            }

            var mappedRecordPayment = _mapper.Map<GetRecordPaymentDto>(recordpayment);
            return Ok(new APIResponse<GetRecordPaymentDto>(mappedRecordPayment, "Recorded retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expense with ID: {ExpenseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }



    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<RecordPaymentUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RecordPaymentUpdateDto recordPaymentUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingRecordPayment = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (existingRecordPayment == null)
            {
                return NotFound(new APIErrorResponse(404, $"RecordPayment with ID {id} not found"));
            }

            _mapper.Map(recordPaymentUpdateDto, existingRecordPayment);
            await _unitOfWork.RecordPaymentRepository.UpdateAsync(existingRecordPayment);
            await _unitOfWork.SaveChangesAsync();

            var updatedRecordPayment = _mapper.Map<GetRecordPaymentDto>(existingRecordPayment);
            return Ok(new APIResponse<GetRecordPaymentDto>(updatedRecordPayment, "Record Payment updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Record Payment with ID: {ExpenseId}", id);
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
            var expense = await _unitOfWork.RecordPaymentRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new APIErrorResponse(404, $"RecordPaymentId with ID {id} not found"));
            }

            expense.IsDeleted = true;
            expense.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>( null, $"RecordPaymentId successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expense with ID: {RecordPaymentId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }









    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid RecordPaymentId, Guid organizationId, IFormFileCollection files)
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

            var source = Path.Combine(organizationId.ToString(), "Recordpayment", RecordPaymentId.ToString());
            var filePaths = await _fileManagementService.AddFileAsync(files, source);

            var RecordPaymentFiles = filePaths.Select(filePath => new RecordPaymentFile
            {
                RecordPaymentId = RecordPaymentId,
                FileName = filePath,
                UploadedAt = DateTime.UtcNow,
                FileSize = files.FirstOrDefault(f => Path.GetFileName(filePath) == f.FileName)?.Length ?? 0
            }).ToList();

            await _unitOfWork.RecordPaymentFileRepository.AddRangeAsync(RecordPaymentFiles.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for Record Payment: {RecordPaymentId}", RecordPaymentId);
            return (false, "Failed to upload files");
        }
    }

}
