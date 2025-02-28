
namespace RentSaaS.Application.DTOs.Property
{
    public class PropertyUpdateDto :PropertyCreateDto
    {
        public Guid Id { get; set; }
        public string? Unite { get; set; }

    }
}
