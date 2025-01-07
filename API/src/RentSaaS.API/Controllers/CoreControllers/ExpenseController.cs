using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;

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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _unitOfWork.ExpenseRepository.GetAll();
        if (expenses == null)
        {
            return NotFound();
        }
        return Ok(expenses);
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.GetById(id);
        if (expense != null)
        {
            return Ok(expense);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            _logger.LogInformation("Create new expense", expense.Id);
            await _unitOfWork.ExpenseRepository.Add(expense);
            await _unitOfWork.SaveChangesAsync();

            return Ok(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new expense, expense street #{AddresId}", expense.Id);
            return new JsonResult($"error on creating new expense {expense.Id}") { StatusCode = 500 };
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var expense = await _unitOfWork.ExpenseRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (expense != null)
        {
            expense.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(id);
    }
}
