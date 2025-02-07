namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertizePropDto : BaseEntityDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string? Unite { get; set; }
        public List<string> Advertising { get; set; } = new List<string>();

    }
}
