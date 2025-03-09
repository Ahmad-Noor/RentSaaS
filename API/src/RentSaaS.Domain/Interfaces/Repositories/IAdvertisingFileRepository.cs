using RentSaaS.Domain.Entities;
using RentSaaS.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Domain.Interfaces.Repositories
{
   public interface IAdvertisingFileRepository : IRepository<AdvertisingFile>
    {
    }
}
