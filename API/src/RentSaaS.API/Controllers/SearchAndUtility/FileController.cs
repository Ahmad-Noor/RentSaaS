using AutoMapper;
using RentSaaS.Domain;
using RentSaaS.API.Controllers;


namespace YourNamespace.Controllers.Utility
{
    public class FileController:BaseControllery
    {

        private readonly ILogger<FileController> _logger;
        public FileController(ILogger<FileController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Your code here
    }
}
