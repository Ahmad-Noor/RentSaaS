using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentSaaS.API.DTOs.ExpenseFileDto;

 public class ExpenseFileCreateDto
{
    [Required]
    public Guid ExpenseId { get; set; }

    [Required(ErrorMessage = "At least one file is required.")]
    [MaxLength(5, ErrorMessage = "You can upload a maximum of 5 files.")]
    public List<IFormFile> ?Files { get; set; }
}
