using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Integration
{
    public class ThirdPartyController : BaseControllery
    {

        private readonly ILogger<ThirdPartyController> _logger;
        public ThirdPartyController(ILogger<ThirdPartyController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
