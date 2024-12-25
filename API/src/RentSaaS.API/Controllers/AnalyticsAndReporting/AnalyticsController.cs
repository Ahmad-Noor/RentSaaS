// AnalyticsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ExpensesController : BaseApiController
{
    //private readonly IAnalyticsService _analyticsService;

    public ExpensesController(
        ILogger<ExpensesController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,IAnalyticsService analyticsService
        )
        : base(logger, unitOfWork, mapper)
    {
        //_analyticsService = analyticsService;
    }

    //[HttpGet("overview")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnalyticsOverviewDto))]
    //public async Task<ActionResult<AnalyticsOverviewDto>> GetAnalyticsOverview(
    //    [FromQuery] DateRangeDto dateRange)
    //{
    //    try
    //    {
    //        var overview = await _analyticsService.GetAnalyticsOverviewAsync(
    //            dateRange,
    //            CurrentUserId);
    //        return Ok(overview);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving analytics overview");
    //        return StatusCode(500, "An error occurred while retrieving analytics overview");
    //    }
    //}

    //[HttpGet("revenue")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RevenueAnalyticsDto))]
    //public async Task<ActionResult<RevenueAnalyticsDto>> GetRevenueAnalytics(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetRevenueAnalyticsAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving revenue analytics");
    //        return StatusCode(500, "An error occurred while retrieving revenue analytics");
    //    }
    //}

    //[HttpGet("occupancy")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccupancyAnalyticsDto))]
    //public async Task<ActionResult<OccupancyAnalyticsDto>> GetOccupancyAnalytics(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetOccupancyAnalyticsAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving occupancy analytics");
    //        return StatusCode(500, "An error occurred while retrieving occupancy analytics");
    //    }
    //}

    //[HttpGet("maintenance")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaintenanceAnalyticsDto))]
    //public async Task<ActionResult<MaintenanceAnalyticsDto>> GetMaintenanceAnalytics(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetMaintenanceAnalyticsAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving maintenance analytics");
    //        return StatusCode(500, "An error occurred while retrieving maintenance analytics");
    //    }
    //}

    //[HttpGet("tenant")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantAnalyticsDto))]
    //public async Task<ActionResult<TenantAnalyticsDto>> GetTenantAnalytics(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetTenantAnalyticsAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant analytics");
    //        return StatusCode(500, "An error occurred while retrieving tenant analytics");
    //    }
    //}

    //[HttpGet("financial")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinancialAnalyticsDto))]
    //public async Task<ActionResult<FinancialAnalyticsDto>> GetFinancialAnalytics(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetFinancialAnalyticsAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving financial analytics");
    //        return StatusCode(500, "An error occurred while retrieving financial analytics");
    //    }
    //}

    //[HttpGet("forecasts")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForecastAnalyticsDto))]
    //public async Task<ActionResult<ForecastAnalyticsDto>> GetForecastAnalytics(
    //    [FromQuery] ForecastRequestDto request)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetForecastAnalyticsAsync(
    //            request,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving forecast analytics");
    //        return StatusCode(500, "An error occurred while retrieving forecast analytics");
    //    }
    //}

    //[HttpGet("trends")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrendAnalyticsDto))]
    //public async Task<ActionResult<TrendAnalyticsDto>> GetTrendAnalytics(
    //    [FromQuery] TrendAnalyticsRequestDto request)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetTrendAnalyticsAsync(
    //            request,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving trend analytics");
    //        return StatusCode(500, "An error occurred while retrieving trend analytics");
    //    }
    //}

    //[HttpGet("benchmarks")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BenchmarkAnalyticsDto))]
    //public async Task<ActionResult<BenchmarkAnalyticsDto>> GetBenchmarkAnalytics(
    //    [FromQuery] BenchmarkRequestDto request)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetBenchmarkAnalyticsAsync(
    //            request,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving benchmark analytics");
    //        return StatusCode(500, "An error occurred while retrieving benchmark analytics");
    //    }
    //}

    //[HttpGet("custom")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomAnalyticsDto))]
    //public async Task<ActionResult<CustomAnalyticsDto>> GetCustomAnalytics(
    //    [FromQuery] CustomAnalyticsRequestDto request)
    //{
    //    try
    //    {
    //        var analytics = await _analyticsService.GetCustomAnalyticsAsync(
    //            request,
    //            CurrentUserId);
    //        return Ok(analytics);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving custom analytics");
    //        return StatusCode(500, "An error occurred while retrieving custom analytics");
    //    }
    //}

    //[HttpPost("export")]
    //[Authorize(Roles = "Admin,Landlord")]
    //public async Task<IActionResult> ExportAnalytics(
    //    [FromBody] AnalyticsExportRequestDto request)
    //{
    //    try
    //    {
    //        var fileContent = await _analyticsService.ExportAnalyticsAsync(
    //            request,
    //            CurrentUserId);

    //        var fileName = $"analytics_{DateTime.UtcNow:yyyyMMddHHmmss}.{request.Format.ToString().ToLower()}";

    //        return File(
    //            fileContent,
    //            GetContentType(request.Format),
    //            fileName);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error exporting analytics");
    //        return StatusCode(500, "An error occurred while exporting analytics");
    //    }
    //}

    //[HttpGet("dashboards")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AnalyticsDashboardDto>))]
    //public async Task<ActionResult<List<AnalyticsDashboardDto>>> GetDashboards()
    //{
    //    try
    //    {
    //        var dashboards = await _analyticsService.GetDashboardsAsync(CurrentUserId);
    //        return Ok(dashboards);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving analytics dashboards");
    //        return StatusCode(500, "An error occurred while retrieving dashboards");
    //    }
    //}

    //[HttpPost("dashboards")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AnalyticsDashboardDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<AnalyticsDashboardDto>> CreateDashboard(
    //    [FromBody] CreateDashboardRequestDto request)
    //{
    //    try
    //    {
    //        var dashboard = await _analyticsService.CreateDashboardAsync(
    //            request,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetDashboards),
    //            new { id = dashboard.Id },
    //            dashboard);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating analytics dashboard");
    //        return StatusCode(500, "An error occurred while creating dashboard");
    //    }
    //}

    //private string GetContentType(ExportFormat format)
    //{
    //    return format switch
    //    {
    //        ExportFormat.PDF => "application/pdf",
    //        ExportFormat.EXCEL => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    //        ExportFormat.CSV => "text/csv",
    //        _ => "application/octet-stream"
    //    };
    //}
}