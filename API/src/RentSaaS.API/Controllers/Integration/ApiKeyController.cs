using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Integration
{
    public class ApiKeyController : BaseControllery
    {

        private readonly ILogger<ApiKeyController> _logger;
        public ApiKeyController(ILogger<ApiKeyController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
