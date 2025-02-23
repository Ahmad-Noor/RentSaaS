using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentSaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPayment
{
    public class RecordPaymentCreateDto
    {

        public Guid? PropertyId { get; set; }

        public decimal? Amount { get; set; }

        public Guid? TenantId { get; set; }


        public int? ReferenceNumber { get; set; }

        public string? Description { get; set; }
        public string? PaymentType { get; set; }

        public IFormFileCollection? ReceiptsFiles { get; set; }

    }
}
