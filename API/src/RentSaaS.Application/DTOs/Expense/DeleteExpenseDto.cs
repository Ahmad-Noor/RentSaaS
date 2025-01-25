namespace RentSaaS.Application.DTOs.Expense
{
    public class DeleteExpenseDto
    {
        public Guid Id { get; set; }
        public string? Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
