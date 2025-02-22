using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.Core;

public class LandlordController  :BaseControllery
{

    private readonly ILogger<LandlordController> _logger;
    public LandlordController(ILogger<LandlordController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
       _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    // Implementation here
}
