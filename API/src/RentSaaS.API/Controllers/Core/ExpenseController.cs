using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Application.Services;
using RentSaaS.Application.DTOs.Property;

namespace RentSaaS.API.Controllers.Core;

public class ExpenseController : BaseControllery
{
    private readonly ILogger<ExpenseController> _logger;
    private readonly IFileManagmentService fileManagmentService;
    public ExpenseController(ILogger<ExpenseController> logger, IUnitOfWork unitOfWork, IMapper mapper, IFileManagmentService fileManagmentService) : base(unitOfWork, mapper)
    {
        _logger = logger;
        this.fileManagmentService = fileManagmentService;
    }

    [HttpPost, Route("add")]
    [ProducesResponseType(typeof(APIResponse<Expense>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    //[Consumes("multipart/form-data")]
    public async Task<IActionResult> Add(ExpenseCreateDTO expenseCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var expense = _mapper.Map<Expense>(expenseCreateDto);
            _logger.LogInformation("Creating new expense with ID: {ExpenseId}", expense.Id);

            await _unitOfWork.ExpenseRepository.Add(expense);
            await _unitOfWork.SaveChangesAsync();

            if (expenseCreateDto.ReceiptsFiles != null && expenseCreateDto.ReceiptsFiles.Count() > 0)
            {
                (bool IsSuccess, string ErrorMessage) fileUploadResult = await UploadFiles(expense.Id, expense.OrganizationId, expenseCreateDto.ReceiptsFiles);
                if (!fileUploadResult.IsSuccess)
                {
                    return BadRequest(fileUploadResult.ErrorMessage);
                }
            }

            return Ok(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new expense with ID: {ExpenseId}", ex);
            return StatusCode(500, $"Error creating new expense {ex}");
        }
    }
 
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(APIResponse<List<PropertyGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var expenses = await _unitOfWork.ExpenseRepository.GetAll();
            if (expenses == null)
            {
                return NotFound(new APIErrorResponse(404));
            }

            var expensesMapper = _mapper.Map<List<GetExpenseDto>>(expenses);
            return Ok(new APIResponse<List<GetExpenseDto>>(true, "All Data For Expenses", expensesMapper));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all expenses");
            return StatusCode(500, new APIErrorResponse(500, "Internal server error occurred while fetching expenses"));
        }
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<Expense>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var expensesById = await _unitOfWork.ExpenseRepository.GetById(id);
        if (expensesById == null)
        {
            return NotFound(new APIErrorResponse(404));
        }

        var expense = _mapper.Map<GetExpenseByIdDto>(expensesById);


        return Ok(expense);
    }

    [HttpPut]
    [Route("update/{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<Expense>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    public async Task<IActionResult> Update([FromRoute] Guid id, ExpenseUpdateDTO expenseUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var expensesById = await _unitOfWork.ExpenseRepository.GetById(id);

        if (expensesById == null)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Updating expense with ID: {ExpenseId}", expensesById.Id);

            var expenseupdate = _mapper.Map(expenseUpdateDto, expensesById);

            _logger.LogInformation("Updated expense: {@Expense}", expensesById);


            await _unitOfWork.ExpenseRepository.Update(expensesById);
            await _unitOfWork.SaveChangesAsync();

            return Ok(expenseupdate); // Return the updated expense
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating expense with ID: {ExpenseId}", expensesById.Id);
            return StatusCode(500, $"Error updating expense {expensesById.Id}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<Expense>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (expense == null)
        {
            return NotFound(new APIErrorResponse(404));
        }

        // Mark the expense as deleted
        expense.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync();

        // Create the DTO for the response
        var deleteExpenseDto = new DeleteExpenseDto
        {
            Id = expense.Id,
            Message = "Expense successfully deleted.",
            IsDeleted = expense.IsDeleted ?? true

        };

        return Ok(deleteExpenseDto);
    }
    //private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid expenseId, Guid organizationId, List<IFormFile> files)
    //{
    //    if (files.Count > 5)
    //    {
    //        return (false, "You can upload up to 5 files.");
    //    }

    //    var orgDirectory = Path.Combine("Uploads", "Expenses", organizationId.ToString());
    //    if (!Directory.Exists(orgDirectory))
    //    {
    //        Directory.CreateDirectory(orgDirectory);
    //    }

    //    var expenseFiles = new List<ExpenseFile>();
    //    foreach (var file in files)
    //    {
    //        var filePath = Path.Combine(orgDirectory, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
    //        using (var stream = new FileStream(filePath, FileMode.Create))
    //        {
    //            await file.CopyToAsync(stream);
    //        }

    //        expenseFiles.Add(new ExpenseFile
    //        {
    //            ExpenseId = expenseId,
    //            FileName = file.FileName
    //        });
    //    }
    //    await _unitOfWork.ExpenseFileRepository.AddRangeAsync(expenseFiles.ToArray());
    //    await _unitOfWork.SaveChangesAsync();

    //    return (true, string.Empty);
    //}    
    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid expenseId, Guid organizationId, IFormFileCollection? files)
    {
        var source = Path.Combine(organizationId.ToString(), "Expenses", expenseId.ToString());
        var filePaths = await fileManagmentService.AddFileAsync(files, source);

        var expenseFiles = filePaths.Select(filePath => new ExpenseFile
        {
            ExpenseId = expenseId,
            FileName = filePath
        }).ToList();

        await _unitOfWork.ExpenseFileRepository.AddRangeAsync(expenseFiles.ToArray());
        await _unitOfWork.SaveChangesAsync();

        return (true, string.Empty);
    }
}