using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ApplicationAndScreening
{
    public class ScreeningController : BaseControllery
    {

        private readonly ILogger<ScreeningController> _logger;
        public ScreeningController(ILogger<ScreeningController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
