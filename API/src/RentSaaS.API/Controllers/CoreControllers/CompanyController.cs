using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.ApiResponse;
using RentSaaS.API.Dto.Company;
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
    private readonly IMapper _Mapper;

    public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork,IMapper Mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }


    #region Create Company
    [Authorize]
    [HttpPost("Add")]
    [ProducesResponseType(typeof(ApiResponse<CompanyCreateDto>), 200)]
    public async Task<IActionResult> Add([FromBody] CompanyCreateDto CompanyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var Company = _Mapper.Map<Company>(CompanyDto);

        try
        {

        var resulte =    await _unitOfWork.CompanyRepository.Add(Company);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<CompanyCreateDto>(true,"Company Is Create Success", CompanyDto));
        }
        catch (Exception)
        {
            return new JsonResult($"error on creating new Company {Company.Id}") { StatusCode = 500 };
        }
    }

    #endregion












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
