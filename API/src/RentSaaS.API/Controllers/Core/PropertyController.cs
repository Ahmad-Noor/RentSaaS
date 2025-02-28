using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.Domain.Entities;
using RentSaaS.API.APIResponse;
using RentSaaS.Application.DTOs.Property;
using Microsoft.AspNetCore.Authorization;

namespace RentSaaS.API.Controllers.Core;

public class PropertyController : BaseControllery
{ 
    private readonly ILogger<PropertyController> _logger;

    public PropertyController(ILogger<PropertyController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
     
    [HttpPost]
    [ProducesResponseType(typeof(APIResponse<PropertyCreateDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] PropertyCreateDto propertyDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid property data"));
            }
            var property = _mapper.Map<Property>(propertyDto);

            await _unitOfWork.PropertyRepository.AddAsync(property);
            await _unitOfWork.SaveChangesAsync();


            var createdProperty = _mapper.Map<PropertyGetDto>(property);
            return CreatedAtAction(nameof(GetById), new { id = property.Id },
                new APIResponse<PropertyGetDto>(createdProperty, "Property created successfully"));
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error creating property");
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(APIResponse<List<PropertyGetDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _unitOfWork.PropertyRepository.AsQueryable().Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedAt);

            var (items, pagination) = await query.ToPaginatedListAsync(page, pageSize);

            var mappedItems = _mapper.Map<List<PropertyGetDto>>(items);

            return Ok(new APIResponse<List<PropertyGetDto>>(mappedItems, "Property retrieved successfully")
            {
                Pagination = pagination
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all property");
            return StatusCode(500, new APIErrorResponse(500, "An unexpected error occurred"));
        }
    }
    [Authorize]
    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyGetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound(new APIErrorResponse(404, $"Property with ID {id} not found"));
            }

            var mappedProperty = _mapper.Map<PropertyGetDto>(property);
            return Ok(new APIResponse<PropertyGetDto>(mappedProperty, "Property retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property with ID: {PropertyId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyUpdateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] PropertyUpdateDto propertyDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIErrorResponse(400, "Invalid update data"));
            }

            var existingProperty = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (existingProperty == null)
            {
                return NotFound(new APIErrorResponse(404, $"Property with ID {id} not found"));
            }

            _mapper.Map(propertyDto, existingProperty);
            await _unitOfWork.PropertyRepository.UpdateAsync(existingProperty);
            await _unitOfWork.SaveChangesAsync();

            var updatedProperty = _mapper.Map<PropertyGetDto>(existingProperty);
            return Ok(new APIResponse<PropertyGetDto>(updatedProperty, "Property updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating property with ID: {PropertyId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }
    }
      
    [HttpDelete]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(APIResponse<PropertyCreateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var property = await _unitOfWork.PropertyRepository.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound(new APIErrorResponse(404, $"Property with ID {id} not found"));
            }

            property.IsDeleted = true;
            property.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new APIResponse<string>(null, $"Property successfully deleted"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting property with ID: {PropertyId}", id);
            return StatusCode(500, new APIErrorResponse(500, DefaultErrorMessage));
        }

    }
     
}
