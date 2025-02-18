using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using RentSaaS.Domain.Common;
namespace RentSaaS.Domain.Entities;

public class Expense : BaseEntity
{
    public string? ExpenseType { get; set; }

    [ForeignKey("Property")]
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }

    [Precision(18, 2)]
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }



    [ForeignKey("company")]
    public Guid? CompanyId { get; set; }
    public Company?Company { get; set; }
    public string? PaymentSchedule { get; set; }
    public string? Category { get; set; }

    public string? Details { get; set; }
    public bool? IsPaid { get; set; }
    public string[]? ReceiptsFiles { get; set; }

    public ICollection<ExpenseFile>? ExpenseFiles { get; set; }

}

