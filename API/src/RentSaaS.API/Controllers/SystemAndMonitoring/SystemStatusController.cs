using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SystemAndMonitoring
{
    public class SystemStatusController : BaseControllery
    {

        private readonly ILogger<SystemStatusController> _logger;
        public SystemStatusController(ILogger<SystemStatusController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
