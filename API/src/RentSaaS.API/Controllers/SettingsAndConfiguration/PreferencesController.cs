using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SettingsAndConfiguration
{
    public class PreferencesController : BaseControllery
    {

        private readonly ILogger<PreferencesController> _logger;
        public PreferencesController(ILogger<PreferencesController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
