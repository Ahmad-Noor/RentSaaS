using AutoMapper;
using RentSaaS.Domain;

namespace RentSaaS.API.Controllers.CoreControllers
{
    public class ChatController : BaseControllery
    {

        private readonly ILogger<ChatController> _logger;
        public ChatController(ILogger<ChatController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // Class implementation
    }
}