using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.BillingAndSubscription
{
    public class PricingController : BaseControllery
    {

        private readonly ILogger<PricingController> _logger;
        public PricingController(ILogger<PricingController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
