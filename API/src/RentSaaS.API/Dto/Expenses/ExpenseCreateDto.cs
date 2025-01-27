using System.ComponentModel.DataAnnotations;

namespace RentSaaS.API.Dto.Expenses
{
    public class ExpenseCreateDto
    {

        [Required(ErrorMessage = "Must Choose Property")]
        public Guid PropertyId { get; set; }


        [Required(ErrorMessage = "Must Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Must Enter Due Date")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Must Enter Valid Organization ")]

        public Guid OrganizationId { get; set; }
        [Required(ErrorMessage = "Must Enter who persone add This Expense")]

        public Guid CreatedBy { get; set; }

        public string? ExpenseType { get; set; }

        public string? PaymentSchedule { get; set; }
        public string? Category { get; set; }

        public string? Details { get; set; }
        public bool? IsPaid { get; set; }


        public IFormFile? ReceiptsFiles { get; set; }

        //public string[]? ReceiptsFiles { get; set; }
    }
}
