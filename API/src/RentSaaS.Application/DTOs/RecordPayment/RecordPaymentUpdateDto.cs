using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPayment
{
    public class RecordPaymentUpdateDto
    {

        public Guid? PropertyId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public Guid? TenantId { get; set; }


        public int? ReferenceNumber { get; set; }

        public string? Description { get; set; }
        public string? PaymentType { get; set; }

    }
}
