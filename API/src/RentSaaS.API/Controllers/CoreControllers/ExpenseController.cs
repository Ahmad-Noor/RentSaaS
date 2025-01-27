using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.API.Dto.Expenses;
using RentSaaS.API.DTOs.ExpenseFileDto;
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

    public ExpenseController(ILogger<ExpenseController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }




    #region Create
    [HttpPost]
    [Route("add")]
    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    public async Task<IActionResult> Add([FromBody] ExpenseCreateDto expenseCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                ExpenseType = expenseCreateDto.ExpenseType,
                PropertyId = expenseCreateDto.PropertyId,
                Amount = expenseCreateDto.Amount,
                DueDate = expenseCreateDto.DueDate,
                PaymentSchedule = expenseCreateDto.PaymentSchedule,
                Category = expenseCreateDto.Category,
                Details = expenseCreateDto.Details,
                IsPaid = expenseCreateDto.IsPaid,
                OrganizationId = expenseCreateDto.OrganizationId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = expenseCreateDto.CreatedBy
            };

            _logger.LogInformation("Creating new expense with ID: {ExpenseId}", expense.Id);

            await _unitOfWork.ExpenseRepository.Add(expense);
            await _unitOfWork.SaveChangesAsync();

            //if (expenseCreateDto.Files != null && expenseCreateDto.Files.Count > 0)
            //{
            //    var fileUploadResult = await UploadFiles(expense.Id, expense.OrganizationId, expenseCreateDto.Files);
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

    #endregion
























    //[Authorize]
    //[HttpGet]
    //[ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    //[ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    //[ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response

    //public async Task<IActionResult> GetAll()
    //{
    //    var expenses = await _unitOfWork.ExpenseRepository.GetAll();
    //    if (expenses == null || !expenses.Any())
    //    {
    //        return NotFound(new ApiErrorResponses(404));
    //    }

    //    var expenseDtos = expenses.Select(expense => new GetExpenseDto
    //    {
    //        Id = expense.Id,
    //        ExpenseType = expense.ExpenseType,
    //        PaymentSchedule = expense.PaymentSchedule,
    //        PropertyId = expense.PropertyId,
    //        Category = expense.Category,
    //        Amount = expense.Amount,
    //        DueDate = expense.DueDate,
    //        Details = expense.Details,
    //        IsPaid = expense.IsPaid,
    //        ReceiptsFiles = expense.ReceiptsFiles,
    //        //PropertyName = expense.MyPropertyid?.Unite

    //    }).ToList();

    //    return Ok(expenseDtos);
    //}









    //[HttpGet]
    //[Authorize]
    //[Route("{id:Guid}")]
    //[ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    //[ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    //[ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    //public async Task<IActionResult> GetById([FromRoute] Guid id)
    //{
    //    var expense = await _unitOfWork.ExpenseRepository.GetById(id);
    //    if (expense == null)
    //    {
    //        return NotFound(new ApiErrorResponses(404));
    //    }

    //    var expenseDto = new GetExpenseByIdDto
    //    {
    //        Id = expense.Id,
    //        ExpenseType = expense.ExpenseType,
    //        PaymentSchedule = expense.PaymentSchedule,
    //        PropertyId = expense.PropertyId,
    //        Category = expense.Category,
    //        Amount = expense.Amount,
    //        DueDate = expense.DueDate,
    //        Details = expense.Details,
    //        IsPaid = expense.IsPaid,
    //        ReceiptsFiles = expense.ReceiptsFiles,
    //        PropertyName = expense.MyPropertyid?.Unite
    //    };

    //    return Ok(expenseDto);
    //}

    #region OldMethods
    //[HttpPost]
    //public async Task<IActionResult> Add([FromBody] Expense expense)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    try
    //    {
    //        _logger.LogInformation("Create new expense", expense.Id);
    //        await _unitOfWork.ExpenseRepository.Add(expense);
    //        await _unitOfWork.SaveChangesAsync();

    //        return Ok(expense);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "error on creating new expense, expense street #{AddresId}", expense.Id);
    //        return new JsonResult($"error on creating new expense {expense.Id}") { StatusCode = 500 };
    //    }
    //}

    //[Authorize]
    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(Guid id, Expense expense)
    //{
    //    if (id != expense.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();
    //} 
    #endregion






















    //private async Task<(bool IsSuccess, string ErrorMessage)> UploadFiles(Guid expenseId, Guid organizationId, List<IFormFile> files)
    //{
    //    if (files.Count > 5)
    //    {
    //        return (false, "You can upload up to 5 files.");
    //    }

    //    var orgDirectory = Path.Combine("Uploads/Expenses", organizationId.ToString());
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

    //    foreach (var expenseFile in expenseFiles)
    //    {
    //        await _unitOfWork.ExpenseFileRepository.Add(expenseFile);
    //    }
    //    await _unitOfWork.SaveChangesAsync();

    //    return (true, string.Empty);
    //}














    //    [HttpPut]
    //    [Route("update")]
    //    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    //    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    //    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    //    public async Task<IActionResult> Update([FromForm] Expense expense, [FromForm] ExpenseFileCreateDto fileDto)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        try
    //        {
    //            _logger.LogInformation("Updating expense with ID: {ExpenseId}", expense.Id);

    //            await _unitOfWork.ExpenseRepository.Update(expense);
    //            await _unitOfWork.SaveChangesAsync();

    //            if (fileDto.Files != null && fileDto.Files.Count > 0)
    //            {
    //                if (fileDto.Files.Count > 5)
    //                {
    //                    return BadRequest("You can upload up to 5 files.");
    //                }

    //                var orgDirectory = Path.Combine("Uploads/Expenses", expense.OrganizationId.ToString());
    //                if (!Directory.Exists(orgDirectory))
    //                {
    //                    Directory.CreateDirectory(orgDirectory);
    //                }

    //                var expenseFiles = new List<ExpenseFile>();
    //                foreach (var file in fileDto.Files)
    //                {
    //                    var filePath = Path.Combine(orgDirectory, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
    //                    using (var stream = new FileStream(filePath, FileMode.Create))
    //                    {
    //                        await file.CopyToAsync(stream);
    //                    }

    //                    expenseFiles.Add(new ExpenseFile
    //                    {
    //                        ExpenseId = expense.Id,
    //                        FileName = file.FileName
    //                    });
    //                }

    //                foreach (var expenseFile in expenseFiles)
    //                {
    //                    await _unitOfWork.ExpenseFileRepository.Add(expenseFile);
    //                }
    //                await _unitOfWork.SaveChangesAsync();
    //            }

    //            return Ok(expense);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error updating expense with ID: {ExpenseId}", expense.Id);
    //            return StatusCode(500, $"Error updating expense {expense.Id}");
    //        }
    //    }


    //    [Authorize]
    //    [HttpDelete("{id}")]
    //    [ProducesResponseType(typeof(ApiResponse<Expense>), 200)] // Success response
    //    [ProducesResponseType(typeof(ApiErrorResponses), 404)] // Not found response
    //    [ProducesResponseType(typeof(ApiErrorResponses), 400)] // Bad request response
    //    public async Task<IActionResult> DeleteAsync(Guid id)
    //    {
    //        var expense = await _unitOfWork.ExpenseRepository.FirstOrDefaultAsync(w => w.Id == id);
    //        if (expense == null)
    //        {
    //            return NotFound(new ApiErrorResponses(404));
    //        }

    //        // Mark the expense as deleted
    //        expense.IsDeleted = true;
    //        await _unitOfWork.SaveChangesAsync();

    //        // Create the DTO for the response
    //        var deleteExpenseDto = new DeleteExpenseDto
    //        {
    //            Id = expense.Id,
    //            Message = "Expense successfully deleted.",
    //            IsDeleted = expense.IsDeleted ?? true

    //        };

    //        return Ok(deleteExpenseDto);
    //    }

}
