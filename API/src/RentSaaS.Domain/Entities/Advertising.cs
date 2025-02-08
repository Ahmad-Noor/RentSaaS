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
        [Required]
        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }

        public string Platform { get; set; } = null!;

        public int Leads {  get; set; }
        public int Views { get; set; }

        public Property? Property { get; set; }
    }
}
