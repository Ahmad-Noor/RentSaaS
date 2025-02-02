namespace RentSaaS.Application.DTOs.Expense;

public class GetExpenseDto
{
    public Guid Id { get; set; }
    public string? ExpenseType { get; set; }
    public string? PaymentSchedule { get; set; }
    public Guid? PropertyId { get; set; }
    public string? Category { get; set; }
    public Guid? CompanyId { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Details { get; set; }
    public bool? IsPaid { get; set; }
     public string? PropertyName { get; set; }
}
