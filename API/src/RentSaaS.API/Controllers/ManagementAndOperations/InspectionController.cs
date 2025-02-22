using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ManagementAndOperations
{
    public class InspectionController : BaseControllery
    {

        private readonly ILogger<InspectionController> _logger;
        public InspectionController(ILogger<InspectionController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
