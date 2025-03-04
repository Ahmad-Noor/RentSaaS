using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.Application.DTOs.RecordPayment;

public class RecordPaymentCreateDto
{

    public Guid? PropertyId { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal? Amount { get; set; }

    public Guid? TenantId { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public string? Details { get; set; }

    public int? ReferenceNumber { get; set; }

    public string? Description { get; set; }
    public string? PaymentType { get; set; }

    public IFormFileCollection? Files { get; set; }

}
