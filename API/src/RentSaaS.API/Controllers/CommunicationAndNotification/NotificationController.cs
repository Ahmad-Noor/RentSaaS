// NotificationController.cs
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
public class NotificationController : BaseApiController
{
    //private readonly INotificationService _notificationService;

    public NotificationController(
        ILogger<NotificationController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,
        //INotificationService notificationService
        )
        : base(logger, unitOfWork, mapper)
    {
        //_notificationService = notificationService;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<NotificationDto>))]
    //public async Task<ActionResult<PaginatedResponse<NotificationDto>>> GetNotifications(
    //    [FromQuery] NotificationFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var notifications = await _notificationService.GetNotificationsAsync(
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);
    //        return Ok(notifications);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notifications");
    //        return StatusCode(500, "An error occurred while retrieving notifications");
    //    }
    //}

    //[HttpGet("unread-count")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    //public async Task<ActionResult<int>> GetUnreadCount()
    //{
    //    try
    //    {
    //        var count = await _notificationService.GetUnreadCountAsync(CurrentUserId);
    //        return Ok(count);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving unread notification count");
    //        return StatusCode(500, "An error occurred while retrieving unread count");
    //    }
    //}

    //[HttpPut("{id:guid}/read")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> MarkAsRead(Guid id)
    //{
    //    try
    //    {
    //        await _notificationService.MarkAsReadAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error marking notification as read");
    //        return StatusCode(500, "An error occurred while marking notification as read");
    //    }
    //}

    //[HttpPut("read-all")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public async Task<IActionResult> MarkAllAsRead()
    //{
    //    try
    //    {
    //        await _notificationService.MarkAllAsReadAsync(CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error marking all notifications as read");
    //        return StatusCode(500, "An error occurred while marking all notifications as read");
    //    }
    //}

    //[HttpDelete("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteNotification(Guid id)
    //{
    //    try
    //    {
    //        await _notificationService.DeleteNotificationAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting notification");
    //        return StatusCode(500, "An error occurred while deleting notification");
    //    }
    //}

    //[HttpDelete]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public async Task<IActionResult> DeleteAllNotifications(
    //    [FromQuery] NotificationDeleteFilterDto filter)
    //{
    //    try
    //    {
    //        await _notificationService.DeleteAllNotificationsAsync(filter, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting all notifications");
    //        return StatusCode(500, "An error occurred while deleting all notifications");
    //    }
    //}

    //[HttpGet("preferences")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationPreferencesDto))]
    //public async Task<ActionResult<NotificationPreferencesDto>> GetPreferences()
    //{
    //    try
    //    {
    //        var preferences = await _notificationService.GetPreferencesAsync(CurrentUserId);
    //        return Ok(preferences);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notification preferences");
    //        return StatusCode(500, "An error occurred while retrieving preferences");
    //    }
    //}

    //[HttpPut("preferences")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdatePreferences(
    //    [FromBody] UpdateNotificationPreferencesDto preferences)
    //{
    //    try
    //    {
    //        await _notificationService.UpdatePreferencesAsync(preferences, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating notification preferences");
    //        return StatusCode(500, "An error occurred while updating preferences");
    //    }
    //}

    //[HttpGet("channels")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationChannelDto>))]
    //public async Task<ActionResult<List<NotificationChannelDto>>> GetChannels()
    //{
    //    try
    //    {
    //        var channels = await _notificationService.GetChannelsAsync(CurrentUserId);
    //        return Ok(channels);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notification channels");
    //        return StatusCode(500, "An error occurred while retrieving channels");
    //    }
    //}

    //[HttpPut("channels")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateChannels(
    //    [FromBody] UpdateNotificationChannelsDto channels)
    //{
    //    try
    //    {
    //        await _notificationService.UpdateChannelsAsync(channels, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating notification channels");
    //        return StatusCode(500, "An error occurred while updating channels");
    //    }
    //}

    //[HttpPost("test")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> SendTestNotification(
    //    [FromBody] TestNotificationRequestDto request)
    //{
    //    try
    //    {
    //        await _notificationService.SendTestNotificationAsync(request, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error sending test notification");
    //        return StatusCode(500, "An error occurred while sending test notification");
    //    }
    //}

    //[HttpGet("templates")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationTemplateDto>))]
    //public async Task<ActionResult<List<NotificationTemplateDto>>> GetTemplates()
    //{
    //    try
    //    {
    //        var templates = await _notificationService.GetTemplatesAsync();
    //        return Ok(templates);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notification templates");
    //        return StatusCode(500, "An error occurred while retrieving templates");
    //    }
    //}

    //[HttpPost("templates")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotificationTemplateDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<NotificationTemplateDto>> CreateTemplate(
    //    [FromBody] CreateNotificationTemplateDto template)
    //{
    //    try
    //    {
    //        var createdTemplate = await _notificationService.CreateTemplateAsync(template);
    //        return CreatedAtAction(
    //            nameof(GetTemplates),
    //            new { id = createdTemplate.Id },
    //            createdTemplate);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating notification template");
    //        return StatusCode(500, "An error occurred while creating template");
    //    }
    //}

    //[HttpPut("templates/{id:guid}")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateTemplate(
    //    Guid id,
    //    [FromBody] UpdateNotificationTemplateDto template)
    //{
    //    try
    //    {
    //        await _notificationService.UpdateTemplateAsync(id, template);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating notification template");
    //        return StatusCode(500, "An error occurred while updating template");
    //    }
    //}

    //[HttpDelete("templates/{id:guid}")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteTemplate(Guid id)
    //{
    //    try
    //    {
    //        await _notificationService.DeleteTemplateAsync(id);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting notification template");
    //        return StatusCode(500, "An error occurred while deleting template");
    //    }
    //}

    //[HttpGet("statistics")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationStatisticsDto))]
    //public async Task<ActionResult<NotificationStatisticsDto>> GetStatistics(
    //    [FromQuery] DateRangeDto dateRange)
    //{
    //    try
    //    {
    //        var statistics = await _notificationService.GetStatisticsAsync(dateRange);
    //        return Ok(statistics);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notification statistics");
    //        return StatusCode(500, "An error occurred while retrieving statistics");
    //    }
    //}
}