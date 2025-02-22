using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Core;

public class PaymentController :BaseControllery
{

    private readonly ILogger<PaymentController> _logger;
    public PaymentController(ILogger<PaymentController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }
    // Implementation here
}
