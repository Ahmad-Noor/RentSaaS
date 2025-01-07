using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
namespace RentSaaS.Domain.Entities;

public class Expense : BaseEntity
    {

        public string? ExpenseType { get; set; }
        public string? PaymentSchedule { get; set; }
        [ForeignKey("MyPropertyid")]
        public Guid? PropertyId { get; set; }
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Details { get; set; }
        public bool? IsPaid { get; set; }
        public string[]? ReceiptsFiles { get; set; }

        public Property? MyPropertyid { get; set; }
    }

