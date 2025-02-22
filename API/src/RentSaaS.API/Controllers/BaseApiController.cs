using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using RentSaaS.Domain;
using System.Security.Claims;

namespace RentSaaS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ILogger<BaseApiController> _logger;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseApiController(
        ILogger<BaseApiController> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    protected string CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}