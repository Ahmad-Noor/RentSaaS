using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.API.DTOs;
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SearchController : BaseApiController
{
    //private readonly ISearchService _searchService;

    public SearchController(
        ILogger<SearchController> logger
        ,IUnitOfWork unitOfWork,
        IMapper mapper
        //,ISearchService searchService
        
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_searchService = searchService;
    }

    //[HttpGet("global")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GlobalSearchResultDto))]
    //public async Task<ActionResult<GlobalSearchResultDto>> GlobalSearch(
    //    [FromQuery] string query,
    //    [FromQuery] List<string> categories = null)
    //{
    //    try
    //    {
    //        var results = await _searchService.GlobalSearchAsync(
    //            query,
    //            categories,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error performing global search");
    //        return StatusCode(500, "An error occurred while performing search");
    //    }
    //}

    //[HttpGet("properties")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<PropertySearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<PropertySearchResultDto>>> SearchProperties(
    //    [FromQuery] PropertySearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchPropertiesAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching properties");
    //        return StatusCode(500, "An error occurred while searching properties");
    //    }
    //}
    // [HttpGet("tenants")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<TenantSearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<TenantSearchResultDto>>> SearchTenants(
    //    [FromQuery] TenantSearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchTenantsAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching tenants");
    //        return StatusCode(500, "An error occurred while searching tenants");
    //    }
    //}

    //[HttpGet("documents")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<DocumentSearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<DocumentSearchResultDto>>> SearchDocuments(
    //    [FromQuery] DocumentSearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchDocumentsAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching documents");
    //        return StatusCode(500, "An error occurred while searching documents");
    //    }
    //}

    //[HttpGet("maintenance-requests")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MaintenanceRequestSearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<MaintenanceRequestSearchResultDto>>> SearchMaintenanceRequests(
    //    [FromQuery] MaintenanceRequestSearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchMaintenanceRequestsAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching maintenance requests");
    //        return StatusCode(500, "An error occurred while searching maintenance requests");
    //    }
    //}

    //[HttpGet("payments")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<PaymentSearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<PaymentSearchResultDto>>> SearchPayments(
    //    [FromQuery] PaymentSearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchPaymentsAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching payments");
    //        return StatusCode(500, "An error occurred while searching payments");
    //    }
    //}

    //[HttpGet("leases")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<LeaseSearchResultDto>))]
    //public async Task<ActionResult<PaginatedResponse<LeaseSearchResultDto>>> SearchLeases(
    //    [FromQuery] LeaseSearchFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var results = await _searchService.SearchLeasesAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error searching leases");
    //        return StatusCode(500, "An error occurred while searching leases");
    //    }
    //}

    //[HttpGet("suggestions")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchSuggestionDto>))]
    //public async Task<ActionResult<List<SearchSuggestionDto>>> GetSearchSuggestions(
    //    [FromQuery] string query,
    //    [FromQuery] string category = null)
    //{
    //    try
    //    {
    //        var suggestions = await _searchService.GetSearchSuggestionsAsync(
    //            query,
    //            category,
    //            CurrentUserId);
    //        return Ok(suggestions);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error getting search suggestions");
    //        return StatusCode(500, "An error occurred while getting search suggestions");
    //    }
    //}

    //[HttpGet("advanced")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdvancedSearchResultDto))]
    //public async Task<ActionResult<AdvancedSearchResultDto>> AdvancedSearch(
    //    [FromQuery] AdvancedSearchFilterDto filter)
    //{
    //    try
    //    {
    //        var results = await _searchService.AdvancedSearchAsync(
    //            filter,
    //            CurrentUserId);
    //        return Ok(results);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error performing advanced search");
    //        return StatusCode(500, "An error occurred while performing advanced search");
    //    }
    //}

    //[HttpGet("recent")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecentSearchDto>))]
    //public async Task<ActionResult<List<RecentSearchDto>>> GetRecentSearches()
    //{
    //    try
    //    {
    //        var recentSearches = await _searchService.GetRecentSearchesAsync(CurrentUserId);
    //        return Ok(recentSearches);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving recent searches");
    //        return StatusCode(500, "An error occurred while retrieving recent searches");
    //    }
    //}

    //[HttpDelete("recent")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public async Task<IActionResult> ClearRecentSearches()
    //{
    //    try
    //    {
    //        await _searchService.ClearRecentSearchesAsync(CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error clearing recent searches");
    //        return StatusCode(500, "An error occurred while clearing recent searches");
    //    }
    //}

    //[HttpGet("saved")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SavedSearchDto>))]
    //public async Task<ActionResult<List<SavedSearchDto>>> GetSavedSearches()
    //{
    //    try
    //    {
    //        var savedSearches = await _searchService.GetSavedSearchesAsync(CurrentUserId);
    //        return Ok(savedSearches);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving saved searches");
    //        return StatusCode(500, "An error occurred while retrieving saved searches");
    //    }
    //}

    //[HttpPost("saved")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SavedSearchDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<SavedSearchDto>> SaveSearch(
    //    [FromBody] SaveSearchRequestDto saveRequest)
    //{
    //    try
    //    {
    //        var savedSearch = await _searchService.SaveSearchAsync(
    //            saveRequest,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetSavedSearches),
    //            new { id = savedSearch.Id },
    //            savedSearch);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error saving search");
    //        return StatusCode(500, "An error occurred while saving the search");
    //    }
    //}

    //[HttpDelete("saved/{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteSavedSearch(Guid id)
    //{
    //    try
    //    {
    //        await _searchService.DeleteSavedSearchAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting saved search");
    //        return StatusCode(500, "An error occurred while deleting the saved search");
    //    }
    //}
}