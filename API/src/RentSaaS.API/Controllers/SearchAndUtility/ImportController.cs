using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Controllers;

namespace YourNamespace.Controllers.Utility
{
    public class ImportController:BaseControllery
    {

        private readonly ILogger<ImportController> _logger;
        public ImportController(ILogger<ImportController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
