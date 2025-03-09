using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentSaaS.Domain.Entities
{
   public class AdvertisingFile:BaseEntity
    {
        [ForeignKey(nameof(Advertising))]
        public Guid AdvertisingId { get; set; }
        public string? FileName { get; set; }

        public Advertising? Advertising { get; set; }
        public DateTime UploadedAt { get; set; }
        public long FileSize { get; set; }
    }
}
