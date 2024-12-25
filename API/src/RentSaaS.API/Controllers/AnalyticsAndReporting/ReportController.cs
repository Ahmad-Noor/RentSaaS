// ReportController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers; 
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportController : BaseApiController
{
    //private readonly IReportService _reportService;
    //private readonly IReportGeneratorService _reportGeneratorService;

    public ReportController(
        ILogger<ReportController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,
        //IReportService reportService,
        //IReportGeneratorService reportGeneratorService
        
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_reportService = reportService;
        //_reportGeneratorService = reportGeneratorService;
    }

    //[HttpGet("financial")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinancialReportDto))]
    //public async Task<ActionResult<FinancialReportDto>> GetFinancialReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetFinancialReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating financial report");
    //        return StatusCode(500, "An error occurred while generating financial report");
    //    }
    //}

    //[HttpGet("occupancy")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccupancyReportDto))]
    //public async Task<ActionResult<OccupancyReportDto>> GetOccupancyReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetOccupancyReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating occupancy report");
    //        return StatusCode(500, "An error occurred while generating occupancy report");
    //    }
    //}

    //[HttpGet("maintenance")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaintenanceReportDto))]
    //public async Task<ActionResult<MaintenanceReportDto>> GetMaintenanceReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetMaintenanceReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating maintenance report");
    //        return StatusCode(500, "An error occurred while generating maintenance report");
    //    }
    //}

    //[HttpGet("tenant-analytics")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantAnalyticsReportDto))]
    //public async Task<ActionResult<TenantAnalyticsReportDto>> GetTenantAnalyticsReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetTenantAnalyticsReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating tenant analytics report");
    //        return StatusCode(500, "An error occurred while generating tenant analytics report");
    //    }
    //}

    //[HttpGet("revenue-forecast")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RevenueForecastReportDto))]
    //public async Task<ActionResult<RevenueForecastReportDto>> GetRevenueForecastReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetRevenueForecastReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating revenue forecast report");
    //        return StatusCode(500, "An error occurred while generating revenue forecast report");
    //    }
    //}

    //[HttpGet("property-performance")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyPerformanceReportDto))]
    //public async Task<ActionResult<PropertyPerformanceReportDto>> GetPropertyPerformanceReport(
    //    [FromQuery] DateRangeDto dateRange,
    //    [FromQuery] List<Guid> propertyIds = null)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetPropertyPerformanceReportAsync(
    //            dateRange,
    //            propertyIds,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating property performance report");
    //        return StatusCode(500, "An error occurred while generating property performance report");
    //    }
    //}

    //[HttpGet("custom")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomReportDto))]
    //public async Task<ActionResult<CustomReportDto>> GetCustomReport(
    //    [FromQuery] CustomReportRequestDto request)
    //{
    //    try
    //    {
    //        var report = await _reportService.GetCustomReportAsync(
    //            request,
    //            CurrentUserId);
    //        return Ok(report);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error generating custom report");
    //        return StatusCode(500, "An error occurred while generating custom report");
    //    }
    //}

    //[HttpPost("export")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> ExportReport(
    //    [FromBody] ReportExportRequestDto request)
    //{
    //    try
    //    {
    //        var fileContent = await _reportGeneratorService.GenerateReportFileAsync(
    //            request,
    //            CurrentUserId);

    //        var fileName = $"report_{DateTime.UtcNow:yyyyMMddHHmmss}.{request.Format.ToString().ToLower()}";

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
    //        _logger.LogError(ex, "Error exporting report");
    //        return StatusCode(500, "An error occurred while exporting report");
    //    }
    //}

    //[HttpGet("scheduled")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ScheduledReportDto>))]
    //public async Task<ActionResult<List<ScheduledReportDto>>> GetScheduledReports()
    //{
    //    try
    //    {
    //        var reports = await _reportService.GetScheduledReportsAsync(CurrentUserId);
    //        return Ok(reports);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving scheduled reports");
    //        return StatusCode(500, "An error occurred while retrieving scheduled reports");
    //    }
    //}

    //[HttpPost("schedule")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ScheduledReportDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<ScheduledReportDto>> ScheduleReport(
    //    [FromBody] ScheduleReportRequestDto request)
    //{
    //    try
    //    {
    //        var scheduledReport = await _reportService.ScheduleReportAsync(
    //            request,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetScheduledReports),
    //            new { id = scheduledReport.Id },
    //            scheduledReport);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error scheduling report");
    //        return StatusCode(500, "An error occurred while scheduling report");
    //    }
    //}

    //[HttpDelete("scheduled/{id:guid}")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteScheduledReport(Guid id)
    //{
    //    try
    //    {
    //        await _reportService.DeleteScheduledReportAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting scheduled report");
    //        return StatusCode(500, "An error occurred while deleting scheduled report");
    //    }
    //}

    //private string GetContentType(ReportFormat format)
    //{
    //    return format switch
    //    {
    //        ReportFormat.PDF => "application/pdf",
    //        ReportFormat.EXCEL => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    //        ReportFormat.CSV => "text/csv",
    //        _ => "application/octet-stream"
    //    };
    //}
}