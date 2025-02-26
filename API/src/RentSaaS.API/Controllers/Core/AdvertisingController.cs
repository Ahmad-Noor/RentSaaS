using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.APIResponse;
using RentSaaS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using RentSaaS.Application.DTOs.Advertising;

namespace RentSaaS.API.Controllers.Core;

public class AdvertisingController : BaseControllery
{

    private readonly ILogger<AdvertisingController> _logger;

    public AdvertisingController(ILogger<AdvertisingController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<AdvertisingGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.AdvertisingRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<AdvertisingGetDto>>(items);

            return Ok(new APIResponse<List<AdvertisingGetDto>>(mappedItems, "Advertising retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all advertising");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }


    [HttpGet]
    [Authorize]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var advertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (advertising == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertising with ID {id} not found"));
            }

            var mappedAdvertising = _mapper.Map<AdvertisingGetDto>(advertising);
            return Ok(new APIResponse<AdvertisingGetDto>(mappedAdvertising, "Advertising retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving advertising with ID: {AdvertisingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] AdvertisingCreateDto advertisingDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid advertising data"));
            }
            var advertising = _mapper.Map<Advertising>(advertisingDto);

            await _unitOfWork.AdvertisingRepository.AddAsync(advertising);
            await _unitOfWork.SaveChangesAsync();


            var createdAdvertising = _mapper.Map<AdvertisingGetDto>(advertising);
            return CreatedAtAction(nameof(GetById), new { id = advertising.Id },
                new APIResponse<AdvertisingGetDto>(createdAdvertising, "advertising created successfully"));

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error creating advertising");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AdvertisingUpdateDto advertisingDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingAdvertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (existingAdvertising == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertising with ID {id} not found"));
            }

            _mapper.Map(advertisingDto, existingAdvertising);
            await _unitOfWork.AdvertisingRepository.UpdateAsync(existingAdvertising);
            await _unitOfWork.SaveChangesAsync();

            var updatedAdvertising = _mapper.Map<AdvertisingGetDto>(existingAdvertising);
            return Ok(new APIResponse<AdvertisingGetDto>(updatedAdvertising, "Advertising updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating advertising with ID: {AdvertisingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(APIResponse<AdvertisingCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var advertising = await _unitOfWork.AdvertisingRepository.GetByIdAsync(id);
            if (advertising == null)
            {
                return NotFound(new APIErrorResponse(404, $"Advertising with ID {id} not found"));
            }

            advertising.IsDeleted = true;
            advertising.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Advertising successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting advertising with ID: {AdvertisingId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
}
