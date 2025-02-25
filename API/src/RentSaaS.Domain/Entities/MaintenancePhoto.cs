using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities
{
    public class MaintenancePhoto : BaseEntity
    {
        [ForeignKey(nameof(Expense))]
        public Guid MaintenanceId { get; set; }
        public string ?PhotoName { get; set; }

        public Maintenance ?Maintenance { get; set; }
        public DateTime UploadedAt { get; set; }
        public long PhotoSize { get; set; }
    }

}
