using Microsoft.EntityFrameworkCore;
using RentSaaS.Application.DTOs.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPayment
{
    public class RecordPaymentByIdDto
    {

        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string? PaymentType { get; set; }
  
   
  
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Details { get; set; }
  
        public List<PaymentfileDto> Files { get; set; }
    


    }
}
