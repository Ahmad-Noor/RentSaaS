
using RentSaaS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Domain.Entities
{
    public class ApplicationAndLeads : BaseEntity
    {
        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }
        public string? ApplicantEmail { get; set; }
        public int PhoneNumber {  get; set; }
        public string? Message {  get; set; }

        public bool? Requestbackground {  get; set; }
        public bool? Requestcredit {  get; set; }

        public Property? Property { get; set; }
    }
}
