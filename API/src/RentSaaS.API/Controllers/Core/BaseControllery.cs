using AutoMapper;
using RentSaaS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RentSaaS.API.Controllers.Core;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseControllery : ControllerBase
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public BaseControllery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
}
