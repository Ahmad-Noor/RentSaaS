namespace RentSaaS.Application.DTOs.Address
{
    public class AddressGetDto
    {
        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }
        public string? Apartment { get; init; }

        public string? POBox { get; init; }

        public string? Line2 { get; init; }

        public string? Country { get; set; }

        //public Guid OrganizationId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public Guid CreatedBy { get; set; }
        //public DateTime? LastModifiedAt { get; set; }
        //public Guid? LastModifiedBy { get; set; }
        //public bool? IsDeleted { get; set; }
        //public DateTime? DeletedAt { get; set; }
        //public Guid? DeletedBy { get; set; }

        //public string? Note { get; set; }
    }
}
