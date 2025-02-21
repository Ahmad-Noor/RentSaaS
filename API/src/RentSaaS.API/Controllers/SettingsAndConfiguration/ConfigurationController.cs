using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SettingsAndConfiguration
{
    public class ConfigurationController : BaseControllery
    {

        private readonly ILogger<ConfigurationController> _logger;
        public ConfigurationController(ILogger<ConfigurationController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
