namespace RentSaaS.API.Dto.Expenses
{
    public class ExpenseGetByIdDto
    {
        public Guid Id { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool? IsPaid { get; set; }
        public string PaymentSchedule { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public Guid PropertyId { get; set; }
        public string[] ReceiptsFiles { get; set; }
    }
}
