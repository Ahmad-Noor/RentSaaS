using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.BillingAndSubscription
{
    public class BillingController : BaseControllery
    {

        private readonly ILogger<BillingController> _logger;
        public BillingController(ILogger<BillingController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
