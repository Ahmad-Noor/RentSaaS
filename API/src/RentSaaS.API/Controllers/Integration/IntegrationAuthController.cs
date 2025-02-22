using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Integration
{
    public class IntegrationAuthController : BaseControllery
    {

        private readonly ILogger<IntegrationAuthController> _logger;
        public IntegrationAuthController(ILogger<IntegrationAuthController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
