using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.ApiErrorResponse;
using RentSaaS.API.ApiResponse;
using RentSaaS.Application.DTOs.Address;

namespace RentSaaS.API.Controllers.CoreControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AddressController : ControllerBase
{
    // add comment for github
    private readonly ILogger<AddressController> _logger;   
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;
    public AddressController(ILogger<AddressController> logger, IUnitOfWork unitOfWork,IMapper Mapper)
    {
        _logger = logger;  
        _unitOfWork = unitOfWork;
        _Mapper = Mapper;
    }

    #region GetAll

    //[Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<AddressGetDto>>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _unitOfWork.AddressRepository.GetAll();
        if (countries == null)
        {
            return NotFound(new ApiErrorResponses(404));
        }
        var CountryMapper = _Mapper.Map<List<AddressGetDto>>(countries);
        return Ok(new ApiResponse<List<AddressGetDto>>(true, "All Data For Country", CountryMapper));
    }

    #endregion


    #region Get By Id

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<AddressGetDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var address = await _unitOfWork.AddressRepository.GetById(id);
        var CountryMapper = _Mapper.Map<AddressGetDto>(address);
        if (address != null)
        {
            return Ok(new ApiResponse<AddressGetDto>(true, "All Data For Country", CountryMapper));
        }
        return NotFound();
    }

    #endregion


    #region Add Address

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AddressCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Add([FromBody] AddressCreateDto addressDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var address = _Mapper.Map<Address>(addressDto);

        try
        {
            _logger.LogInformation("Create new address, address street #{AddresStreet}", address.Street);
            await _unitOfWork.AddressRepository.Add(address);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new ApiResponse<AddressCreateDto>(true, "Address Is Create Success", addressDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new address, address street #{AddresStreet}", address.Street);
            return new JsonResult($"error on creating new address {address.Street}") { StatusCode = 500 };
        }
    }

    #endregion


    #region Update

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressUpdateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> Update(Guid id, AddressUpdateDto addressDto)
    {
        if (id != addressDto.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    #endregion


    #region Delete

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AddressCreateDto>), 200)]
    [ProducesResponseType(typeof(ApiErrorResponses), 400)]
    [ProducesResponseType(typeof(ApiErrorResponses), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var address = await _unitOfWork.AddressRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (address != null)
        {
            address.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(new ApiErrorResponses(404));
    }

    #endregion

}
