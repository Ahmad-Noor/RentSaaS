// SettingsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentSaaS.API.Controllers;
using RentSaaS.Domain;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SettingsController : BaseApiController
{
    //private readonly ISettingsService _settingsService;
    //private readonly IEmailSettingsService _emailSettingsService;
    //private readonly INotificationSettingsService _notificationSettingsService;

    public SettingsController(
        ILogger<SettingsController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,ISettingsService settingsService,
        //IEmailSettingsService emailSettingsService,
        //INotificationSettingsService notificationSettingsService
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_settingsService = settingsService;
        //_emailSettingsService = emailSettingsService;
        //_notificationSettingsService = notificationSettingsService;
    }

    //[HttpGet("company")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanySettingsDto))]
    //public async Task<ActionResult<CompanySettingsDto>> GetCompanySettings()
    //{
    //    try
    //    {
    //        var settings = await _settingsService.GetCompanySettingsAsync(CurrentUserId);
    //        return Ok(settings);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving company settings");
    //        return StatusCode(500, "An error occurred while retrieving company settings");
    //    }
    //}

    //[HttpPut("company")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateCompanySettings([FromBody] CompanySettingsUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _settingsService.UpdateCompanySettingsAsync(updateDto, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating company settings");
    //        return StatusCode(500, "An error occurred while updating company settings");
    //    }
    //}

    //[HttpGet("email")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailSettingsDto))]
    //public async Task<ActionResult<EmailSettingsDto>> GetEmailSettings()
    //{
    //    try
    //    {
    //        var settings = await _emailSettingsService.GetEmailSettingsAsync(CurrentUserId);
    //        return Ok(settings);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving email settings");
    //        return StatusCode(500, "An error occurred while retrieving email settings");
    //    }
    //}

    //[HttpPut("email")]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateEmailSettings([FromBody] EmailSettingsUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _emailSettingsService.UpdateEmailSettingsAsync(updateDto, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating email settings");
    //        return StatusCode(500, "An error occurred while updating email settings");
    //    }
    //}

    //[HttpGet("notifications")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationSettingsDto))]
    //public async Task<ActionResult<NotificationSettingsDto>> GetNotificationSettings()
    //{
    //    try
    //    {
    //        var settings = await _notificationSettingsService.GetNotificationSettingsAsync(CurrentUserId);
    //        return Ok(settings);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving notification settings");
    //        return StatusCode(500, "An error occurred while retrieving notification settings");
    //    }
    //}

    //[HttpPut("notifications")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateNotificationSettings(
    //    [FromBody] NotificationSettingsUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _notificationSettingsService.UpdateNotificationSettingsAsync(
    //            updateDto,
    //            CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating notification settings");
    //        return StatusCode(500, "An error occurred while updating notification settings");
    //    }
    //}

    //[HttpGet("payment")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentSettingsDto))]
    //public async Task<ActionResult<PaymentSettingsDto>> GetPaymentSettings()
    //{
    //    try
    //    {
    //        var settings = await _settingsService.GetPaymentSettingsAsync(CurrentUserId);
    //        return Ok(settings);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving payment settings");
    //        return StatusCode(500, "An error occurred while retrieving payment settings");
    //    }
    //}

    //[HttpPut("payment")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdatePaymentSettings([FromBody] PaymentSettingsUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _settingsService.UpdatePaymentSettingsAsync(updateDto, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating payment settings");
    //        return StatusCode(500, "An error occurred while updating payment settings");
    //    }
    //}

    //[HttpGet("lease-templates")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LeaseTemplateDto>))]
    //public async Task<ActionResult<List<LeaseTemplateDto>>> GetLeaseTemplates()
    //{
    //    try
    //    {
    //        var templates = await _settingsService.GetLeaseTemplatesAsync(CurrentUserId);
    //        return Ok(templates);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving lease templates");
    //        return StatusCode(500, "An error occurred while retrieving lease templates");
    //    }
    //}

    //[HttpPost("lease-templates")]
    //[Authorize(Roles = "Admin,Landlord")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaseTemplateDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<LeaseTemplateDto>> CreateLeaseTemplate(
    //    [FromBody] LeaseTemplateCreateDto createDto)
    //{
    //    try
    //    {
    //        var template = await _settingsService.CreateLeaseTemplateAsync(
    //            createDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetLeaseTemplates),
    //            new { id = template.Id },
    //            template);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating lease template");
    //        return StatusCode(500, "An error occurred while creating lease template");
    //    }
    //}
}