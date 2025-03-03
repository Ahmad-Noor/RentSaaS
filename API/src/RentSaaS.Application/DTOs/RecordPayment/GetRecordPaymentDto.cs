using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPayment
{
    public class GetRecordPaymentDto
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public Guid? TenantId { get; set; }


        public int? ReferenceNumber { get; set; }

        public string? Description { get; set; }
        public string? PaymentType { get; set; }

        public DateTime DueDate { get; set; }
        public string? Details { get; set; }
        public IFormFileCollection? Files { get; set; }

    }
}
