using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.Expense
{
    public class ExpenseCreateDTO
    {
        public string? ExpenseType { get; set; }
        public Guid? CompanyId { get; set; }
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public string PaymentSchedule { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string? Details { get; set; }

        public bool IsPaid { get; set; }

        public List<IFormFile>? Receipts { get; set; }
        public IFormFileCollection? ReceiptsFiles { get; set; }
    }
}
