using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ApplicationAndScreening
{
    public class ReferenceController : BaseControllery
    {

        private readonly ILogger<ReferenceController> _logger;
        public ReferenceController(ILogger<ReferenceController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
