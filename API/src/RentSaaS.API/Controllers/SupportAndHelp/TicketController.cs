using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SupportAndHelp
{
    public class TicketController : BaseControllery
    {

        private readonly ILogger<TicketController> _logger;
        public TicketController(ILogger<TicketController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
