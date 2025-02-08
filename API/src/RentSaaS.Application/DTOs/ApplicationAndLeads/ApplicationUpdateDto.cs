using RentSaaS.Application.DTOs.Advertising;
using RentSaaS.Application.DTOs.RentApplication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Application.DTOs.RentApplication
{
    public class ApplicationUpdateDto : ApplicationCreateDto
    {
        public Guid Id { get; set; }

    }
}
