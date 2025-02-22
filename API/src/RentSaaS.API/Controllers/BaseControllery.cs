using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RentSaaS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseControllery : ControllerBase
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public const string DefaultErrorMessage = "An unexpected error occurred while processing your request.";
    public BaseControllery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}
