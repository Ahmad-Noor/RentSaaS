using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPayment
{
    public class RecordPaymentByIdDto
    {
        public Guid? PropertyId { get; set; }

        public decimal? Amount { get; set; }

        public Guid? TenantId { get; set; }


        public int? ReferenceNumber { get; set; }

        public string? Description { get; set; }
        public string? PaymentType { get; set; }
    }
}
