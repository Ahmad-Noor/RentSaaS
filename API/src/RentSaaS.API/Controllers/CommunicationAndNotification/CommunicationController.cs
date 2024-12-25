// CommunicationController.cs
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
public class CommunicationController : BaseApiController
{
    //private readonly ICommunicationService _communicationService;
    //private readonly INotificationService _notificationService;

    public CommunicationController(
        ILogger<CommunicationController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        
        //,
        //ICommunicationService communicationService,
        //INotificationService notificationService
        
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_communicationService = communicationService;
        //_notificationService = notificationService;
    }

    //[HttpGet("messages")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MessageDto>))]
    //public async Task<ActionResult<PaginatedResponse<MessageDto>>> GetMessages(
    //    [FromQuery] MessageFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var messages = await _communicationService.GetMessagesAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(messages);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving messages");
    //        return StatusCode(500, "An error occurred while retrieving messages");
    //    }
    //}

    //[HttpGet("messages/{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MessageDetailDto>> GetMessageById(Guid id)
    //{
    //    try
    //    {
    //        var message = await _communicationService.GetMessageByIdAsync(id, CurrentUserId);
    //        if (message == null)
    //        {
    //            return NotFound($"Message with ID {id} not found");
    //        }

    //        return Ok(message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving message {MessageId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the message");
    //    }
    //}

    //[HttpPost("messages")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<MessageDto>> SendMessage([FromBody] MessageCreateDto createDto)
    //{
    //    try
    //    {
    //        var message = await _communicationService.SendMessageAsync(
    //            CurrentUserId,
    //            createDto);

    //        // Send notification to recipients
    //        await _notificationService.NotifyMessageRecipientsAsync(message);

    //        return CreatedAtAction(
    //            nameof(GetMessageById),
    //            new { id = message.Id },
    //            message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error sending message");
    //        return StatusCode(500, "An error occurred while sending the message");
    //    }
    //}

    //[HttpPost("messages/{id:guid}/read")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> MarkAsRead(Guid id)
    //{
    //    try
    //    {
    //        await _communicationService.MarkMessageAsReadAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error marking message as read");
    //        return StatusCode(500, "An error occurred while marking the message as read");
    //    }
    //}

    //[HttpGet("threads")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<ThreadDto>))]
    //public async Task<ActionResult<PaginatedResponse<ThreadDto>>> GetThreads(
    //    [FromQuery] ThreadFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var threads = await _communicationService.GetThreadsAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(threads);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving threads");
    //        return StatusCode(500, "An error occurred while retrieving threads");
    //    }
    //}

    //[HttpGet("threads/{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThreadDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<ThreadDetailDto>> GetThreadById(Guid id)
    //{
    //    try
    //    {
    //        var thread = await _communicationService.GetThreadByIdAsync(id, CurrentUserId);
    //        if (thread == null)
    //        {
    //            return NotFound($"Thread with ID {id} not found");
    //        }

    //        return Ok(thread);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving thread {ThreadId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the thread");
    //    }
    //}

    //[HttpPost("threads")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThreadDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<ThreadDto>> CreateThread([FromBody] ThreadCreateDto createDto)
    //{
    //    try
    //    {
    //        var thread = await _communicationService.CreateThreadAsync(
    //            CurrentUserId,
    //            createDto);

    //        return CreatedAtAction(
    //            nameof(GetThreadById),
    //            new { id = thread.Id },
    //            thread);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating thread");
    //        return StatusCode(500, "An error occurred while creating the thread");
    //    }
    //}

    //[HttpPost("threads/{id:guid}/messages")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MessageDto>> ReplyToThread(
    //    Guid id,
    //    [FromBody] ThreadReplyDto replyDto)
    //{
    //    try
    //    {
    //        var message = await _communicationService.ReplyToThreadAsync(
    //            id,
    //            CurrentUserId,
    //            replyDto);

    //        // Send notification to thread participants
    //        await _notificationService.NotifyThreadParticipantsAsync(id, message);

    //        return CreatedAtAction(
    //            nameof(GetMessageById),
    //            new { id = message.Id },
    //            message);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error replying to thread");
    //        return StatusCode(500, "An error occurred while replying to the thread");
    //    }
    //}

    //[HttpPost("threads/{id:guid}/participants")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> AddThreadParticipants(
    //    Guid id,
    //    [FromBody] AddThreadParticipantsDto participantsDto)
    //{
    //    try
    //    {
    //        await _communicationService.AddThreadParticipantsAsync(
    //            id,
    //            CurrentUserId,
    //            participantsDto);

    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error adding thread participants");
    //        return StatusCode(500, "An error occurred while adding participants");
    //    }
    //}
}