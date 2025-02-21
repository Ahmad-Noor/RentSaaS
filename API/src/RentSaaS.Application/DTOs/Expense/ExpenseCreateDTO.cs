using Microsoft.AspNetCore.Http;

namespace RentSaaS.Application.DTOs.Expense
{
    public class ExpenseCreateDTO
    {
        public string? ExpenseType { get; set; }
        public Guid? PropertyId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? CompanyId { get; set; }
        public string? PaymentSchedule { get; set; }
        public string? Category { get; set; }
        public string? Details { get; set; }
        public bool? IsPaid { get; set; }
        public IFormFileCollection? ReceiptsFiles { get; set; }
    }
}
