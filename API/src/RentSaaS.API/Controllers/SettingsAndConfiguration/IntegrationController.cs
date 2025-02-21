using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SettingsAndConfiguration
{
    public class IntegrationController : BaseControllery
    {

        private readonly ILogger<IntegrationController> _logger;
        public IntegrationController(ILogger<IntegrationController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
