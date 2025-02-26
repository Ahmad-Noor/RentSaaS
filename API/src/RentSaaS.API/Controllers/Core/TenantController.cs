// TenantController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.API.Controllers;
using RentSaaS.API.Controllers.Core;
using RentSaaS.API.DTOs;
using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Application.DTOs.Tenant;
using RentSaaS.Domain;
using RentSaaS.Domain.Entities;

public class TenantController : BaseControllery
{

    private readonly ILogger<TenantController> _logger;


    public TenantController(ILogger<TenantController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Authorize]
    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(typeof(APIResponse<List<TenantGetDto>>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetAll()
    {
        var tenents = await _unitOfWork.tenantRepository.GetAllAsync();

        if (tenents == null)
        {
            return NotFound(new APIErrorResponse(404));
        }
        var tenantMapper = _mapper.Map<List<TenantGetDto>>(tenents);
        return Ok(new APIResponse<List<TenantGetDto>>(tenantMapper, "All Data For tenant"));
    }



    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<TenantGetDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var tenants = await _unitOfWork.tenantRepository.GetByIdAsync(id);
        var tenantMapper = _mapper.Map<TenantGetDto>(tenants);
        if (tenants != null)
        {
            return Ok(new APIResponse<TenantGetDto>(tenantMapper, "All Data For tenant"));
        }
        return NotFound(new APIErrorResponse(404));
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(APIResponse<TenantCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> Add([FromBody] TenantCreateDto tenantCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var tenanting = _mapper.Map<Tenant>(tenantCreateDto);

        try
        {
            _logger.LogInformation("Create new Advertising");
            await _unitOfWork.tenantRepository.AddAsync(tenanting);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<TenantCreateDto>(tenantCreateDto, "tenanting Is Create Success"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error on creating new leases ");
            return new JsonResult($"error on creating new leases") { StatusCode = 500 };
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(APIResponse<TenantUpdateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 404)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    public async Task<IActionResult> Update(Guid id, TenantUpdateDto tenantUpdateDto)
    {
        if (id != tenantUpdateDto.Id)
        {
            return BadRequest();
        }
        
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<TenantCreateDto>), 200)]
    [ProducesResponseType(typeof(APIErrorResponse), 400)]
    [ProducesResponseType(typeof(APIErrorResponse), 500)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var tenanting = await _unitOfWork.tenantRepository.FirstOrDefaultAsync(w => w.Id == id);
        if (tenanting != null)
        {
            tenanting.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            //  return Ok(new APIResponse<AdvertisingCreateDto>(true, "Delete Is Success"));
            return NoContent();
        }
        return NotFound(new APIErrorResponse(404));
    }

}