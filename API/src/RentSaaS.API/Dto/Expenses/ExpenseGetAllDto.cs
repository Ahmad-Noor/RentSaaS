namespace RentSaaS.API.Dto.Expenses
{
    public class ExpenseGetAllDto
    {
        public Guid Id { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool? IsPaid { get; set; }
        public string Category { get; set; }

    }
}
