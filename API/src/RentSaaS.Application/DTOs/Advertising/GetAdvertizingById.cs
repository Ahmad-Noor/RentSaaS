

namespace RentSaaS.Application.DTOs.Advertising
{
   public class GetAdvertizingById
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

        public List<AdvertizingFileDTO>? Files { get; set; }
        public string? PropertyName { get; set; }
    }
}
