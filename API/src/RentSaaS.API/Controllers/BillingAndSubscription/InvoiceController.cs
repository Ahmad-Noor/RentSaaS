using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.BillingAndSubscription
{
    public class InvoiceController : BaseControllery
    {

        private readonly ILogger<InvoiceController> _logger;
        public InvoiceController(ILogger<InvoiceController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
