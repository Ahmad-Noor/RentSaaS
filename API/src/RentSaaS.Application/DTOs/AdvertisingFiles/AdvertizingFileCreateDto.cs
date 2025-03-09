using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.AdvertisingFiles
{
   public class AdvertizingFileCreateDto
    {
        [Required]
        public Guid AdvertizingId { get; set; }

        [Required(ErrorMessage = "At least one file is required.")]
        [MaxLength(5, ErrorMessage = "You can upload a maximum of 5 files.")]
        public List<IFormFile>? Files { get; set; }
    }
}
