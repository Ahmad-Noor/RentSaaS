// DashboardController.cs
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
public class DashboardController : BaseApiController
{
    //private readonly IDashboardService _dashboardService;

    public DashboardController(
        ILogger<DashboardController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,
        //IDashboardService dashboardService
        )
        : base(logger, unitOfWork, mapper)
    {
        //_dashboardService = dashboardService;
    }

    //[HttpGet("summary")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DashboardSummaryDto))]
    //public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
    //{
    //    try
    //    {
    //        var summary = await _dashboardService.GetDashboardSummaryAsync(CurrentUserId);
    //        return Ok(summary);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving dashboard summary");
    //        return StatusCode(500, "An error occurred while retrieving dashboard summary");
    //    }
    //}

    //[HttpGet("financial-overview")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinancialOverviewDto))]
    //public async Task<ActionResult<FinancialOverviewDto>> GetFinancialOverview(
    //    [FromQuery] DateRangeDto dateRange)
    //{
    //    try
    //    {
    //        var overview = await _dashboardService.GetFinancialOverviewAsync(
    //            CurrentUserId,
    //            dateRange);
    //        return Ok(overview);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving financial overview");
    //        return StatusCode(500, "An error occurred while retrieving financial overview");
    //    }
    //}

    //[HttpGet("occupancy-stats")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccupancyStatsDto))]
    //public async Task<ActionResult<OccupancyStatsDto>> GetOccupancyStats()
    //{
    //    try
    //    {
    //        var stats = await _dashboardService.GetOccupancyStatsAsync(CurrentUserId);
    //        return Ok(stats);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving occupancy stats");
    //        return StatusCode(500, "An error occurred while retrieving occupancy stats");
    //    }
    //}

    //[HttpGet("maintenance-overview")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaintenanceOverviewDto))]
    //public async Task<ActionResult<MaintenanceOverviewDto>> GetMaintenanceOverview()
    //{
    //    try
    //    {
    //        var overview = await _dashboardService.GetMaintenanceOverviewAsync(CurrentUserId);
    //        return Ok(overview);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving maintenance overview");
    //        return StatusCode(500, "An error occurred while retrieving maintenance overview");
    //    }
    //}

    //[HttpGet("upcoming-events")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UpcomingEventDto>))]
    //public async Task<ActionResult<List<UpcomingEventDto>>> GetUpcomingEvents()
    //{
    //    try
    //    {
    //        var events = await _dashboardService.GetUpcomingEventsAsync(CurrentUserId);
    //        return Ok(events);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving upcoming events");
    //        return StatusCode(500, "An error occurred while retrieving upcoming events");
    //    }
    //}

    //[HttpGet("recent-activities")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<ActivityLogDto>))]
    //public async Task<ActionResult<PaginatedResponse<ActivityLogDto>>> GetRecentActivities(
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var activities = await _dashboardService.GetRecentActivitiesAsync(
    //            CurrentUserId,
    //            pageNumber,
    //            pageSize);
    //        return Ok(activities);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving recent activities");
    //        return StatusCode(500, "An error occurred while retrieving recent activities");
    //    }
    //}

    //[HttpGet("alerts")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DashboardAlertDto>))]
    //public async Task<ActionResult<List<DashboardAlertDto>>> GetAlerts()
    //{
    //    try
    //    {
    //        var alerts = await _dashboardService.GetDashboardAlertsAsync(CurrentUserId);
    //        return Ok(alerts);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving dashboard alerts");
    //        return StatusCode(500, "An error occurred while retrieving alerts");
    //    }
    //}
}