
using Microsoft.AspNetCore.Http;

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingUpdateDto 
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }

        public decimal? MontholyRent { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public string? Details { get; set; }
        public DateTime? AvailableForm { get; set; }
        public bool? Zillow { get; set; }
        public bool? Trulia { get; set; }
        public bool? Realtor { get; set; }
        public bool? Apartments { get; set; }
        public IFormFileCollection? Files { get; set; }
        public List<string>? FilesToDelete { get; set; }

    }
}
