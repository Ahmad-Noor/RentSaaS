using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Models;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using Microsoft.Extensions.Options;
using RentSaaS.Application.Services;
using RentSaaS.Application.DTOs.Expense;

namespace RentSaaS.API.Controllers.Core;

public class ExpenseController : BaseControllery
{
    private readonly ILogger<ExpenseController> _logger;
    private readonly IFileManagmentService _fileManagementService;
    private readonly FileUploadSettings _fileUploadSettings;

    public ExpenseController(
        ILogger<ExpenseController> logger,
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
    [ProducesResponseType(typeof(APIResponse<List<GetExpenseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.ExpenseRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<GetExpenseDto>>(items);

            return Ok(new APIResponse<List<GetExpenseDto>>(mappedItems, "Expenses retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all expenses");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<GetExpenseByIdDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new APIErrorResponse(404, $"Expense with ID {id} not found"));
            }

            var mappedExpense = _mapper.Map<GetExpenseByIdDto>(expense);
            return Ok(new APIResponse<GetExpenseByIdDto>(mappedExpense, "Expense retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expense with ID: {ExpenseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<ExpenseCreateDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] ExpenseCreateDTO expenseCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid expense data"));
            }

            var expense = _mapper.Map<Expense>(expenseCreateDto);
            await _unitOfWork.ExpenseRepository.AddAsync(expense);

            if (expenseCreateDto.ReceiptsFiles?.Any() == true)
            {
                var (IsSuccess, ErrorMessage) = await UploadFiles(expense.Id, expense.OrganizationId, expenseCreateDto.ReceiptsFiles);
                if (!IsSuccess)
                {
                    return BadRequest(new APIErrorResponse(400, ErrorMessage));
                }
            }

            await _unitOfWork.SaveChangesAsync();

            var createdExpense = _mapper.Map<GetExpenseDto>(expense);
            return CreatedAtAction(nameof(GetById), new { id = expense.Id },
                new APIResponse<GetExpenseDto>(createdExpense, "Expense created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating expense");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }


    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<ExpenseUpdateDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ExpenseUpdateDTO expenseUpdateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingExpense = await _unitOfWork.ExpenseRepository.GetByIdAsync(id);
            if (existingExpense == null)
            {
                return NotFound(new APIErrorResponse(404, $"Expense with ID {id} not found"));
            }

            _mapper.Map(expenseUpdateDto, existingExpense);
            await _unitOfWork.ExpenseRepository.UpdateAsync(existingExpense);
            await _unitOfWork.SaveChangesAsync();

            var updatedExpense = _mapper.Map<GetExpenseDto>(existingExpense);
            return Ok(new APIResponse<GetExpenseDto>(updatedExpense, "Expense updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating expense with ID: {ExpenseId}", id);
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
            var expense = await _unitOfWork.ExpenseRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new APIErrorResponse(404, $"Expense with ID {id} not found"));
            }

            expense.IsDeleted = true;
            expense.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Expense successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expense with ID: {ExpenseId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid expenseId, Guid organizationId, IFormFileCollection files)
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

            var source = Path.Combine(organizationId.ToString(), "Expenses", expenseId.ToString());
            var filePaths = await _fileManagementService.AddFileAsync(files, source);

            var expenseFiles = filePaths.Select(filePath => new ExpenseFile
            {
                ExpenseId = expenseId,
                FileName = filePath,
                UploadedAt = DateTime.UtcNow,
                FileSize = files.FirstOrDefault(f => Path.GetFileName(filePath) == f.FileName)?.Length ?? 0
            }).ToList();

            await _unitOfWork.ExpenseFileRepository.AddRangeAsync(expenseFiles.ToArray());
            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading files for expense: {ExpenseId}", expenseId);
            return (false, "Failed to upload files");
        }
    }
}