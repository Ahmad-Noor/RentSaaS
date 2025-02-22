using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SupportAndHelp
{
    public class SupportController : BaseControllery
    {

        private readonly ILogger<SupportController> _logger;
        public SupportController(ILogger<SupportController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
