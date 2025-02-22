using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.AnalyticsAndReporting
{
    public class StatisticsController:BaseControllery
    {
        private readonly ILogger<StatisticsController> _logger;
        public StatisticsController(ILogger<StatisticsController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Class implementation
    }
}
