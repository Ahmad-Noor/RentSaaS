using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Address;

namespace RentSaaS.API.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AddressController : ControllerBase
{
    // add comment for github
    private readonly ILogger<AddressController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddressController(ILogger<AddressController> logger, IUnitOfWork unitOfWork, IMapper Mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork;
        _mapper = Mapper;
    }

    //[Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<AddressGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _unitOfWork.AddressRepository.GetAllAsync();
        if (countries == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var CountryMapper = _mapper.Map<List<AddressGetDto>>(countries);
        return Ok(new APIResponse<List<AddressGetDto>>(CountryMapper, "All Data For Country"));
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<AddressGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
        var CountryMapper = _mapper.Map<AddressGetDto>(address);
        if (address != null)
        {
            return Ok(new APIResponse<AddressGetDto>(CountryMapper, "All Data For Country" ));
        }
        return NotFound();
    }
    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<AddressCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] AddressCreateDto addressDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var address = _mapper.Map<Address>(addressDto);

        try
        {
            _logger.LogInformation("Create new address, address street #{AddresStreet}", address.Street);
            await _unitOfWork.AddressRepository.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<AddressCreateDto>(addressDto, "Address Is Create Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new address, address street #{AddresStreet}", address.Street);
            return new JsonResult($"error on creating new address {address.Street}") { StatusCode = 500 };
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(APIResponse<AddressUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Update(Guid id, AddressUpdateDto addressDto)
    {
        if (id != addressDto.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<AddressCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var address = await _unitOfWork.AddressRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (address != null)
        {
            address.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));
    }

}
