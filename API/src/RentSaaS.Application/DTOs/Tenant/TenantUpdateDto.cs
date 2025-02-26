using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.Tenant
{
    public class TenantUpdateDto:TenantCreateDto
    {
        public Guid Id { get; set; }
    }
}
