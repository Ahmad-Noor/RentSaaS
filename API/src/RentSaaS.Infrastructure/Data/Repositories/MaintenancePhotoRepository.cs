using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class MaintenancePhotoRepository : Repository<MaintenancePhoto>, IMaintenancePhotoRepository
{
    public MaintenancePhotoRepository(RentSaaSDBContext dbContext, ILogger<MaintenancePhotoRepository> logger) : base(dbContext, logger)
    {
    }
}
