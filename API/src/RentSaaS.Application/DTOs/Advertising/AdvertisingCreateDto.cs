
namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingCreateDto
    { 
        public Guid? PropertyId { get; set; }
        public string? Platform { get; set; }
        public int Views { get; set; }
        public int Leads { get; set; }
    }
}
