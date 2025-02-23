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
    public class RecordPaymentRepository:Repository<RecordPayment> ,IRecordPaymentRepository
    {

        public RecordPaymentRepository(RentSaaSDBContext dbContext, ILogger<RecordPaymentRepository> logger) : base(dbContext, logger)
        {
            
        }
    }
}
