using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SupportAndHelp
{
    public class FaqController : BaseControllery
    {

        private readonly ILogger<FaqController> _logger;
        public FaqController(ILogger<FaqController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
