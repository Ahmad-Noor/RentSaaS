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
public class DocumentController : BaseApiController
{
    //private readonly IDocumentService _documentService;
    //private readonly IBlobStorageService _blobStorageService;

    public DocumentController(
        ILogger<DocumentController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        //,IDocumentService documentService,
        //IBlobStorageService blobStorageService
        
        )
        : base(logger, unitOfWork, mapper)
    {
        //_documentService = documentService;
        //_blobStorageService = blobStorageService;
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<DocumentDto>))]
    //public async Task<ActionResult<PaginatedResponse<DocumentDto>>> GetAll(
    //    [FromQuery] DocumentFilterDto filter,
    //    [FromQuery] int pageNumber = 1,
    //    [FromQuery] int pageSize = 10)
    //{
    //    try
    //    {
    //        var documents = await _documentService.GetDocumentsAsync(
    //            CurrentUserId,
    //            filter,
    //            pageNumber,
    //            pageSize);

    //        return Ok(documents);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving documents");
    //        return StatusCode(500, "An error occurred while retrieving documents");
    //    }
    //}

    //[HttpGet("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDetailDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<DocumentDetailDto>> GetById(Guid id)
    //{
    //    try
    //    {
    //        var document = await _documentService.GetDocumentByIdAsync(id, CurrentUserId);
    //        if (document == null)
    //        {
    //            return NotFound($"Document with ID {id} not found");
    //        }

    //        return Ok(document);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving document {DocumentId}", id);
    //        return StatusCode(500, "An error occurred while retrieving the document");
    //    }
    //}

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DocumentDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<DocumentDto>> Upload([FromForm] DocumentUploadDto uploadDto)
    //{
    //    try
    //    {
    //        if (uploadDto.File == null || uploadDto.File.Length == 0)
    //        {
    //            return BadRequest("No file was provided");
    //        }

    //        // Validate file type and size
    //        if (!_documentService.IsValidFileType(uploadDto.File.ContentType))
    //        {
    //            return BadRequest("Invalid file type");
    //        }

    //        if (!_documentService.IsValidFileSize(uploadDto.File.Length))
    //        {
    //            return BadRequest("File size exceeds the maximum limit");
    //        }

    //        var document = await _documentService.UploadDocumentAsync(
    //            uploadDto,
    //            CurrentUserId);

    //        return CreatedAtAction(
    //            nameof(GetById),
    //            new { id = document.Id },
    //            document);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error uploading document");
    //        return StatusCode(500, "An error occurred while uploading the document");
    //    }
    //}

    //[HttpGet("{id:guid}/download")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Download(Guid id)
    //{
    //    try
    //    {
    //        var document = await _documentService.GetDocumentByIdAsync(id, CurrentUserId);
    //        if (document == null)
    //        {
    //            return NotFound($"Document with ID {id} not found");
    //        }

    //        var fileStream = await _blobStorageService.DownloadAsync(document.BlobPath);
    //        return File(fileStream, document.ContentType, document.FileName);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error downloading document {DocumentId}", id);
    //        return StatusCode(500, "An error occurred while downloading the document");
    //    }
    //}

    //[HttpPut("{id:guid}/metadata")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateMetadata(
    //    Guid id,
    //    [FromBody] DocumentMetadataUpdateDto updateDto)
    //{
    //    try
    //    {
    //        await _documentService.UpdateDocumentMetadataAsync(
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
    //        _logger.LogError(ex, "Error updating document metadata");
    //        return StatusCode(500, "An error occurred while updating the document metadata");
    //    }
    //}

    //[HttpDelete("{id:guid}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    try
    //    {
    //        await _documentService.DeleteDocumentAsync(id, CurrentUserId);
    //        return NoContent();
    //    }
    //    catch (NotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error deleting document {DocumentId}", id);
    //        return StatusCode(500, "An error occurred while deleting the document");
    //    }
    //}

    //[HttpPost("batch")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DocumentDto>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<List<DocumentDto>>> BatchUpload([FromForm] BatchDocumentUploadDto batchUploadDto)
    //{
    //    try
    //    {
    //        if (batchUploadDto.Files == null || !batchUploadDto.Files.Any())
    //        {
    //            return BadRequest("No files were provided");
    //        }

    //        var documents = await _documentService.BatchUploadDocumentsAsync(
    //            batchUploadDto,
    //            CurrentUserId);

    //        return Ok(documents);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error batch uploading documents");
    //        return StatusCode(500, "An error occurred while uploading the documents");
    //    }
    //}

    //[HttpGet("types")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DocumentTypeDto>))]
    //public async Task<ActionResult<List<DocumentTypeDto>>> GetDocumentTypes()
    //{
    //    try
    //    {
    //        var types = await _documentService.GetDocumentTypesAsync();
    //        return Ok(types);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error retrieving document types");
    //        return StatusCode(500, "An error occurred while retrieving document types");
    //    }
    //}

    //[HttpPost("share")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentShareDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<DocumentShareDto>> ShareDocument([FromBody] DocumentShareRequestDto shareRequest)
    //{
    //    try
    //    {
    //        var shareResult = await _documentService.ShareDocumentAsync(
    //            shareRequest,
    //            CurrentUserId);

    //        return Ok(shareResult);
    //    }
    //    catch (ValidationException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error sharing document");
    //        return StatusCode(500, "An error occurred while sharing the document");
    //    }
    //}
}