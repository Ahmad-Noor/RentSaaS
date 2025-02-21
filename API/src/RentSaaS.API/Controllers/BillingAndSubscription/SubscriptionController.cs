using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.BillingAndSubscription
{
    public class SubscriptionController : BaseControllery
    {

        private readonly ILogger<SubscriptionController> _logger;
        public SubscriptionController(ILogger<SubscriptionController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
