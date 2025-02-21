using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SystemAndMonitoring
{
    public class LogController : BaseControllery
    {

        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
