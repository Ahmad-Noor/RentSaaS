using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Application.DTOs.Company;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;
namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


public class CompanyController : ControllerBase
{
 
    private readonly ILogger<CompanyController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Companies = await _unitOfWork.CompanyRepository.GetAll();
        if (Companies == null)
        {
            return NotFound();
        }
        return Ok(Companies);
    }

    [HttpGet]
    //[Authorize]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var Companies = await _unitOfWork.CompanyRepository.GetById(id);
        if (Companies != null)
        {
            return Ok(Companies);
        }
        return NotFound();
    }



    [Authorize]
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CompanyCreateDto company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }


        var Companies = new Company
        {
            OrganizationId = company.OrganizationId,
            CreatedAt = company.CreatedAt,
            CreatedBy = company.CreatedBy,
            LastModifiedAt = company.LastModifiedAt,
            LastModifiedBy = company.LastModifiedBy,
            IsDeleted = company.IsDeleted,
            DeletedAt = company.DeletedAt,
            DeletedBy = company.DeletedBy,
            Note = company.Note,
            Name = company.Name,
            //Address = company.Address,
            //Phone = company.Phone,
            //Email = company.Email,
            //Website = company.Website,
            //Logo = company.Logo
        };

        try
        {

            await _unitOfWork.CompanyRepository.Add(Companies);
            await _unitOfWork.SaveChangesAsync();

            return Ok(Companies);
        }
        catch (Exception)
        {
            return new JsonResult($"error on creating new Company {Companies.Id}") { StatusCode = 500 };
        }
    }


    //[Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CompanyUpdateDto company)
    {
        if (id != company.Id)
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
        var company = await _unitOfWork.CompanyRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (company != null)
        {
            company.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(id);

    }
}
