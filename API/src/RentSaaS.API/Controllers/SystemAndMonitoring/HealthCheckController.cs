using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SystemAndMonitoring
{
    public class HealthCheckController : BaseControllery
    {

        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILogger<HealthCheckController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
