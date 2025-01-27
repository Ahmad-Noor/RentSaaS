namespace RentSaaS.API.Dto.Expenses
{
    public class ExpenseUpdateDto
    {
       
        public string ExpenseType { get; set; }
        public Guid PropertyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentSchedule { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public bool? IsPaid { get; set; }
        public string[] ?ReceiptsFiles { get; set; }
    }
}
