using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ApplicationAndScreening
{
    public class ApplicationController : BaseControllery
    {

        private readonly ILogger<ApplicationController> _logger;
        public ApplicationController(ILogger<ApplicationController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
