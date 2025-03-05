using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.MaintenancePhotoDto
{
   public class MaintenancePhotoUpdateDto:MaintenancePhotoCreateDto
    {
        public Guid Id { get; set; }
    }
}
