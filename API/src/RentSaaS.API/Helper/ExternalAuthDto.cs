using System.ComponentModel.DataAnnotations;

namespace RentSaaS.API.Helper
{
    public class ExternalAuthDto
    {
        [Required]
        public string IdToken { get; set; }
    }
}
