using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ManagementAndOperations
{
    public class CalendarController : BaseControllery
    {

        private readonly ILogger<CalendarController> _logger;
        public CalendarController(ILogger<CalendarController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
