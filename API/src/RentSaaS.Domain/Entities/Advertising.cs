using RentSaaS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Domain.Entities
{
    public class Advertising : BaseEntity
    {
        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }

        public decimal? MonthlyRent { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public string? Details { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public  string[]? ReceiptsFiles { get; set; }

        public bool? Zillow { get; set; }
        public bool? Trulia { get; set; }
        public bool? Realtor { get; set; }
        public bool? Apartments { get; set; }

        public ICollection<AdvertisingFile>? AdvertisingFiles { get; set; }

        public Property? Property { get; set; }
    }
}
