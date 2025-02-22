using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ApplicationAndScreening
{
    public class BackgroundCheckController : BaseControllery
    {

        private readonly ILogger<BackgroundCheckController> _logger;
        public BackgroundCheckController(ILogger<BackgroundCheckController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
