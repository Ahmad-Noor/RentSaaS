using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.Expense;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;

namespace RentSaaS.API.Controllers.CoreControllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class ExpenseController : Controller
{
    // add comment for github
    private readonly ILogger<ExpenseController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExpenseController(ILogger<ExpenseController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }





    [HttpPost]
    [Route("add")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    
    public async Task<IActionResult> Add([FromForm] ExpenseCreateDTO expenseCreateDto)
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

            //if (expenseCreateDto.ReceiptsFiles != null && expenseCreateDto.ReceiptsFiles.Count > 0)
            //{
            //    var fileUploadResult = await UploadFiles(expense.Id, expense.OrganizationId, expenseCreateDto.ReceiptsFiles);
            //    if (!fileUploadResult.IsSuccess)
            //    {
            //        return BadRequest(fileUploadResult.ErrorMessage);
            //    }
            //}

            return Ok(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new expense with ID: {ExpenseId}", ex);
            return StatusCode(500, $"Error creating new expense {ex}");
        }
    }


    [Authorize]
    [HttpGet]
    [Route("getall")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response

    public async Task<IActionResult> GetAll()
    {
        var expenses = await _unitOfWork.ExpenseRepository.GetAll();
        if (expenses == null || !expenses.Any())
        {
            return NotFound(new ApiErrorResponses(404));
        }
        var expense = _mapper.Map<List<GetExpenseDto>>(expenses);


        return Ok(expense);
    }



    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var expensesById = await _unitOfWork.ExpenseRepository.GetById(id);
        if (expensesById == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }

        var expense = _mapper.Map<GetExpenseByIdDto>(expensesById);


        return Ok(expense);
    }



    [HttpPut]
    [Route("update/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
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


    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (expense == null)
        {
            return NotFound(new ApiErrorResponses(404));
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
    private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid expenseId, Guid organizationId, List<IFormFile> files)
    {
        if (files.Count > 5)
        {
            return (false, "You can upload up to 5 files.");
        }

        var orgDirectory = Path.Combine("Uploads/Expenses", organizationId.ToString());
        if (!Directory.Exists(orgDirectory))
        {
            Directory.CreateDirectory(orgDirectory);
        }

        var expenseFiles = new List<ExpenseFile>();
        foreach (var file in files)
        {
            var filePath = Path.Combine(orgDirectory, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            expenseFiles.Add(new ExpenseFile
            {
                ExpenseId = expenseId,
                FileName = file.FileName
            });
        }

        foreach (var expenseFile in expenseFiles)
        {
            await _unitOfWork.ExpenseFileRepository.Add(expenseFile);
        }
        await _unitOfWork.SaveChangesAsync();

        return (true, string.Empty);
    }
}
