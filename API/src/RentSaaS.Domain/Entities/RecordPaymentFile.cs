using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities
{
    public class RecordPaymentFile : BaseEntity
    {

        [ForeignKey(nameof(RecordPayment))]
        public Guid RecordPaymentId { get; set; }
        public string? FileName { get; set; }

        public RecordPayment? RecordPayment { get; set; }
        public DateTime UploadedAt { get; set; }
        public long FileSize { get; set; }

    

    }
}
