using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.SupportAndHelp
{
    public class HelpController : BaseControllery
    {

        private readonly ILogger<HelpController> _logger;
        public HelpController(ILogger<HelpController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
