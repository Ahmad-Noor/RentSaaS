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
public class TenantController : BaseApiController
{
    //private readonly ITenantService _tenantService;
    //private readonly IBackgroundCheckService _backgroundCheckService;

    public TenantController(
        ILogger<TenantController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,
        //ITenantService tenantService,
        //IBackgroundCheckService backgroundCheckService
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_tenantService = tenantService;
        //_backgroundCheckService = backgroundCheckService;
    }

    //[HttpGet]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<TenantDto>))]
    //public async Task<ActionResult<PaginatedResponse<TenantDto>>> GetAll(
    //    [FromQuery] TenantFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var tenants = await _tenantService.GetTenantsAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(tenants);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenants");
    //        return StatusCode(500, "An error occurred while retrieving tenants");
    //    }
    //}

    //[HttpGet("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<TenantDetailDto>> GetById(Guid id)
    //{
    //    try
    //    {
    //        var tenant = await _tenantService.GetTenantByIdAsync(id, CurrentUserId);
    //        if (tenant == null)
    //        {
    //            return NotFound($"Tenant with ID {id} not found");
    //        }

    //        return Ok(tenant);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant {TenantId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the tenant");
    //    }
    //}

    //[HttpPost]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TenantDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<TenantDto>> Create([FromBody] TenantCreateDto createDto)
    //{
    //    try
    //    {
    //        var tenant = await _tenantService.CreateTenantAsync(
    //            CurrentUserId,
    //            createDto);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id = tenant.Id },
    //            tenant);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error creating tenant");
    //        return StatusCode(500, "An error occurred while creating the tenant");
    //    }
    //}

    //[HttpPut("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Update(
    //    Guid id,
    //    [FromBody] TenantUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _tenantService.UpdateTenantAsync(
    //            id,
    //            updateDto,
    //            CurrentUserId);

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
    //        _logger.LogError(ex, "Error updating tenant");
    //        return StatusCode(500, "An error occurred while updating the tenant");
    //    }
    //}

    //[HttpPost("{id:guid}/background-check")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BackgroundCheckResultDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<BackgroundCheckResultDto>> InitiateBackgroundCheck(
    //    Guid id,
    //    [FromBody] BackgroundCheckRequestDto requestDto)
    //{
    //    try
    //    {
    //        var result = await _backgroundCheckService.InitiateBackgroundCheckAsync(
    //            id,
    //            requestDto,
    //            CurrentUserId);

    //        return Ok(result);
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
    //        _logger.LogError(ex, "Error initiating background check");
    //        return StatusCode(500, "An error occurred while initiating the background check");
    //    }
    //}

    //[HttpGet("{id:guid}/rental-history")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RentalHistoryDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<List<RentalHistoryDto>>> GetRentalHistory(Guid id)
    //{
    //    try
    //    {
    //        var history = await _tenantService.GetTenantRentalHistoryAsync(id, CurrentUserId);
    //        return Ok(history);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant rental history");
    //        return StatusCode(500, "An error occurred while retrieving rental history");
    //    }
    //}

    //[HttpGet("{id:guid}/payment-history")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<PaymentHistoryDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<PaginatedResponse<PaymentHistoryDto>>> GetPaymentHistory(
    //    Guid id,
    //    [FromQuery] PaymentHistoryFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var payments = await _tenantService.GetTenantPaymentHistoryAsync(
    //            id,
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);

    //        return Ok(payments);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant payment history");
    //        return StatusCode(500, "An error occurred while retrieving payment history");
    //    }
    //}

    //[HttpGet("{id:guid}/documents")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TenantDocumentDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<List<TenantDocumentDto>>> GetDocuments(Guid id)
    //{
    //    try
    //    {
    //        var documents = await _tenantService.GetTenantDocumentsAsync(id, CurrentUserId);
    //        return Ok(documents);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant documents");
    //        return StatusCode(500, "An error occurred while retrieving documents");
    //    }
    //}

    //[HttpPost("{id:guid}/documents")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TenantDocumentDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<TenantDocumentDto>> UploadDocument(
    //    Guid id,
    //    [FromForm] TenantDocumentUploadDto uploadDto)
    //{
    //    try
    //    {
    //        var document = await _tenantService.UploadTenantDocumentAsync(
    //            id,
    //            uploadDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetDocuments),
    //            new { id },
    //            document);
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
    //        _logger.LogError(ex, "Error uploading tenant document");
    //        return StatusCode(500, "An error occurred while uploading document");
    //    }
    //}

    //[HttpGet("{id:guid}/maintenance-requests")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<MaintenanceRequestDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<PaginatedResponse<MaintenanceRequestDto>>> GetMaintenanceRequests(
    //    Guid id,
    //    [FromQuery] MaintenanceRequestFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var requests = await _tenantService.GetTenantMaintenanceRequestsAsync(
    //            id,
    //            filter,
    //            pageNumber,
    //            pageSize,
    //            CurrentUserId);

    //        return Ok(requests);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant maintenance requests");
    //        return StatusCode(500, "An error occurred while retrieving maintenance requests");
    //    }
    //}

    //[HttpGet("{id:guid}/lease-agreements")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LeaseAgreementDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<List<LeaseAgreementDto>>> GetLeaseAgreements(Guid id)
    //{
    //    try
    //    {
    //        var leases = await _tenantService.GetTenantLeaseAgreementsAsync(id, CurrentUserId);
    //        return Ok(leases);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant lease agreements");
    //        return StatusCode(500, "An error occurred while retrieving lease agreements");
    //    }
    //}

    //[HttpPost("{id:guid}/verify-income")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncomeVerificationResultDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<IncomeVerificationResultDto>> VerifyIncome(
    //    Guid id,
    //    [FromBody] IncomeVerificationRequestDto requestDto)
    //{
    //    try
    //    {
    //        var result = await _tenantService.VerifyTenantIncomeAsync(
    //            id,
    //            requestDto,
    //            CurrentUserId);

    //        return Ok(result);
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
    //        _logger.LogError(ex, "Error verifying tenant income");
    //        return StatusCode(500, "An error occurred while verifying income");
    //    }
    //}

    //[HttpPost("{id:guid}/credit-check")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditCheckResultDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<CreditCheckResultDto>> InitiateCreditCheck(
    //    Guid id,
    //    [FromBody] CreditCheckRequestDto requestDto)
    //{
    //    try
    //    {
    //        var result = await _tenantService.InitiateCreditCheckAsync(
    //            id,
    //            requestDto,
    //            CurrentUserId);

    //        return Ok(result);
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
    //        _logger.LogError(ex, "Error initiating credit check");
    //        return StatusCode(500, "An error occurred while initiating credit check");
    //    }
    //}

    //[HttpGet("{id:guid}/references")]
    //[Authorize(Roles = "Landlord,Admin")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TenantReferenceDto>))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<List<TenantReferenceDto>>> GetReferences(Guid id)
    //{
    //    try
    //    {
    //        var references = await _tenantService.GetTenantReferencesAsync(id, CurrentUserId);
    //        return Ok(references);
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving tenant references");
    //        return StatusCode(500, "An error occurred while retrieving references");
    //    }
    //}

    //[HttpPost("{id:guid}/references")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TenantReferenceDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<TenantReferenceDto>> AddReference(
    //    Guid id,
    //    [FromBody] TenantReferenceCreateDto createDto)
    //{
    //    try
    //    {
    //        var reference = await _tenantService.AddTenantReferenceAsync(
    //            id,
    //            createDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetReferences),
    //            new { id },
    //            reference);
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
    //        _logger.LogError(ex, "Error adding tenant reference");
    //        return StatusCode(500, "An error occurred while adding reference");
    //    }
    //}
}