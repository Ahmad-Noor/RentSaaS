using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.Company
{
    public class CompanyCreateDto: BaseEntityDto
    {

        public Guid Id { get; set; }  
        public string Name { get; set; } = null!;
        public string? LogoURL { get; set; }
        public bool? ShowLogo { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? ContactId { get; set; }
        public string? CommercialNo { get; set; }
        public string? SiteURL { get; set; }
        public Guid? DefaultCurrencyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


    }
}
