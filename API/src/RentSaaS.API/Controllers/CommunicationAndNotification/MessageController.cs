using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.CoreControllers
{
    public class MessageController : BaseControllery
    {

        private readonly ILogger<MessageController> _logger;
        public MessageController(ILogger<MessageController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _logger = logger;

        }
        // Class implementation
    }
}
