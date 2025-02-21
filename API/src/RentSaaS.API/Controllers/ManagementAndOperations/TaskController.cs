using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.ManagementAndOperations
{
    public class TaskController : BaseControllery
    {

        private readonly ILogger<TaskController> _logger;
        public TaskController(ILogger<TaskController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Your code here
    }
}
