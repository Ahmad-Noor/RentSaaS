using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.ApiErrorResponse;
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
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
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



    #region Delete

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var company = await _unitOfWork.CompanyRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (company != null)
        {
            company.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ApiResponse<CompanyGetDto>(true,"Delete Is Success"));
        }
        return NotFound(new ApiErrorResponses(404));
    }
    #endregion







    #region Get 
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<CompanyGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
        var Companies = await _unitOfWork.CompanyRepository.GetAll();
        if (Companies == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }

        var CompanyMapper=_Mapper.Map<List<CompanyGetDto>>(Companies);
        return Ok(new ApiResponse<List<CompanyGetDto>>(true,"All Data For Company",CompanyMapper));
    }

    #endregion


    #region Get By Id


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CompanyGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var Companies = await _unitOfWork.CompanyRepository.GetById(id);
        var CompanyMapper = _Mapper.Map<CompanyGetDto>(Companies);
        if (Companies != null)

        {
            return Ok(new ApiResponse<CompanyGetDto>(true, "All Data For Company", CompanyMapper));
        }
        return NotFound();
    }




    #endregion


















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
