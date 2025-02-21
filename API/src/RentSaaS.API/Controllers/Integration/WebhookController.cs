using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Integration
{
    public class WebhookController : BaseControllery
    {

        private readonly ILogger<WebhookController> _logger;
        public WebhookController(ILogger<WebhookController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
