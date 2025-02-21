using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SystemAndMonitoring
{
    public class MetricsController : BaseControllery
    {

        private readonly ILogger<MetricsController> _logger;
        public MetricsController(ILogger<MetricsController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
