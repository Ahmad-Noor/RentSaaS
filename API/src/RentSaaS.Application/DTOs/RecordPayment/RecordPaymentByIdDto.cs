namespace RentSaaS.Application.DTOs.RecordPayment;

public class RecordPaymentByIdDto
{ 
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public string? PaymentType { get; set; } 
    public decimal? Amount { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Details { get; set; }

    public List<PaymentfileDto> Files { get; set; } 
}
