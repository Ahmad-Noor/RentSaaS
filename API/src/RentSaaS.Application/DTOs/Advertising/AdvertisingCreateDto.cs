
using Microsoft.AspNetCore.Http;
using RentSaaS.Domain.Entities;

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingCreateDto
    {
        public Guid PropertyId { get; set; }

        public decimal? MonthlyRent { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public string? Details { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public bool? Zillow { get; set; }
        public bool? Trulia { get; set; }
        public bool? Realtor { get; set; }
        public bool? Apartments { get; set; }

        public IFormFileCollection? Files { get; set; }

    }
}
