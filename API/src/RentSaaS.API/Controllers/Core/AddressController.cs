using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Address;
using Microsoft.AspNetCore.Authorization;

namespace RentSaaS.API.Controllers.Core;


public class AddressController : BaseControllery
{

    private readonly ILogger<AddressController> _logger;

    public AddressController(ILogger<AddressController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<AddressGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.AddressRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<AddressGetDto>>(items);

            return Ok(new APIResponse<List<AddressGetDto>>(mappedItems, "Address retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all address");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }

    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<AddressGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound(new APIErrorResponse(404, $"Address with ID {id} not found"));
            }

            var mappedAddress = _mapper.Map<AddressGetDto>(address);
            return Ok(new APIResponse<AddressGetDto>(mappedAddress, "Address retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving address with ID: {AddressId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<AddressCreateDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddressCreateDto addressDto)
    {       
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid address data"));
            }
            var address = _mapper.Map<Address>(addressDto);

            _logger.LogInformation("Create new address, address street #{AddresStreet}", address.Street);
            await _unitOfWork.AddressRepository.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();


            var createdAddress = _mapper.Map<AddressGetDto>(address);
            return CreatedAtAction(nameof(GetById), new { id = address.Id },
                new APIResponse<AddressGetDto>(createdAddress, "Address created successfully"));

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error creating address");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<AddressUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] AddressUpdateDto addressDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingAddress = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (existingAddress == null)
            {
                return NotFound(new APIErrorResponse(404, $"Address with ID {id} not found"));
            }

            _mapper.Map(addressDto, existingAddress);
            await _unitOfWork.AddressRepository.UpdateAsync(existingAddress);
            await _unitOfWork.SaveChangesAsync();

            var updatedAddress = _mapper.Map<AddressGetDto>(existingAddress);
            return Ok(new APIResponse<AddressGetDto>(updatedAddress, "Address updated successfully"));
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error updating address with ID: {AddressId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<AddressCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound(new APIErrorResponse(404, $"Address with ID {id} not found"));
            }

            address.IsDeleted = true;
            address.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Address successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting address with ID: {AddressId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

}
