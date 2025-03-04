using Microsoft.EntityFrameworkCore;
using RentSaaS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Domain.Entities
{
    public class RecordPayment:BaseEntity
    {

        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [ForeignKey("Tenant")]
        public Guid? TenantId { get; set; }

        public Tenant? Tenant { get; set; }

        public int? ReferenceNumber { get; set; }

        public string? Description { get; set; }
        public string? PaymentType { get; set; }

        public string[]? ReceiptsFiles { get; set; }
        public DateTime DueDate { get; set; }
        public string? Details { get; set; }


        public ICollection<RecordPaymentFile>? PaymentFiles { get; set; }
    }
}
