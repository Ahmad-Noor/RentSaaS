using Microsoft.Extensions.Logging;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Infrastructure.Data.Repositories
{
    public class RecordPaymentFileRepository : Repository<RecordPaymentFile>, IRecordPaymentFile
    {
        public RecordPaymentFileRepository(RentSaaSDBContext dbContext, ILogger<RecordPaymentFileRepository> logger) : base(dbContext, logger)
        {
        }
    }

}
