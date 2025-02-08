namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertisingGetDto
    {
        public Guid Id { get; set; }

        public string Platform { get; set; } = null!;

        public int Leads { get; set; }
        public int Views { get; set; }

    }
}
