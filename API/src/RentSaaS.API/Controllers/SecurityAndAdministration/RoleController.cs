using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SecurityAndAdministration
{
    public class RoleController:BaseControllery
    {

        private readonly ILogger<RoleController> _logger;
        public RoleController(ILogger<RoleController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Class implementation
    }
}
