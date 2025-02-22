using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Controllers;

namespace YourNamespace.Controllers.Utility
{
    public class ExportController : BaseControllery
    {

        private readonly ILogger<ExportController> _logger;
        public ExportController(ILogger<ExportController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
