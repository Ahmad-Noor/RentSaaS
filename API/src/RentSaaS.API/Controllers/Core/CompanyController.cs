using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using RentSaaS.Application.Dtos.Company;
using Microsoft.AspNetCore.Authorization;
namespace RentSaaS.API.Controllers.Core;

public class CompanyController : BaseControllery
{
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpPost("Add")]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    [ProducesResponseType(typeof(APIResponse<CompanyCreateDto>), 200)]
    public async Task<IActionResult> Add([FromBody] CompanyCreateDto CompanyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {

            var Company = _mapper.Map<Company>(CompanyDto);
            var resulte = await _unitOfWork.CompanyRepository.Add(Company);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<CompanyCreateDto>(true, "Company Is Create Success", CompanyDto));
        }
        catch (Exception)
        {
            return new JsonResult($"error on creating new Company {CompanyDto.Name}") { StatusCode = 500 };
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<CompanyCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var company = await _unitOfWork.CompanyRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (company != null)
        {
            company.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return Ok(new APIResponse<CompanyGetDto>(true, "Delete Is Success"));
        }
        return NotFound(new APIErrorResponse(404));
    }

    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<CompanyGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var Companies = await _unitOfWork.CompanyRepository.GetAll();
        if (Companies == null)
        {
            return NotFound(new APIErrorResponse(404));
        }

        var CompanyMapper = _mapper.Map<List<CompanyGetDto>>(Companies);
        return Ok(new APIResponse<List<CompanyGetDto>>(true, "All Data For Company", CompanyMapper));
    }


    //[Authorize]
    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(Guid id, CompanyUpdateDto company)
    //{
    //    if (id != company.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();
    //}
}
