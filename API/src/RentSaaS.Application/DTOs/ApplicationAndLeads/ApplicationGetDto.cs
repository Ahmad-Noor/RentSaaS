namespace RentSaaS.Application.DTOs.RentApplication
{
    public class ApplicationGetDto
    {
        public Guid PropertyId { get; set; }

        public string ApplicantEmail { get; set; }
        public int PhoneNumber { get; set; }
        public string? Message { get; set; }
        public bool Requestbackgroundcheck { get; set; }
        public bool Requestcreditreport { get; set; }

        public Guid? OrganizationId { get; set; }
    }
}
