using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.MaintenancePhotoDto
{
   public class MaintenancePhotoCreateDto
    {
        [Required]
        public Guid MaintenanceId { get; set; }

        [Required(ErrorMessage = "At least one photo is required.")]
        [MaxLength(5, ErrorMessage = "You can upload a maximum of 5 phptp.")]
        public List<IFormFile>? Files { get; set; }
    }
}
