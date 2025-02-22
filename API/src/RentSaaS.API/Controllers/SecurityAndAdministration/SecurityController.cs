using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SecurityAndAdministration
{
    public class SecurityController:BaseControllery
    {

        private readonly ILogger<SecurityController> _logger;
        public SecurityController(ILogger<SecurityController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Class implementation
    }
}
