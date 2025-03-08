using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.Lease
{
    public class LeaseGetById
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public decimal RentAmount { get; set; }
        public string? TenantName { get; set; }
        public string? LeaseType { get; set; }
        public string? PropertyName { get; set; }
    }
}
