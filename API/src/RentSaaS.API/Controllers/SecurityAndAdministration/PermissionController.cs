using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SecurityAndAdministration
{
    public class PermissionController:BaseControllery
    {

        private readonly ILogger<PermissionController> _logger;
        public PermissionController(ILogger<PermissionController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Class implementation
    }
}
