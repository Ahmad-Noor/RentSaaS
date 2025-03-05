using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities
{
    public class Maintenance:BaseEntity
    {
        [ForeignKey("Property")]
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public string? IssueType {  get; set; }
        public string? Priority { get; set; }
        public string? Details {  get; set; }
        public DateTime DueDate { get; set; }
        public string[]? File { get; set; }
        public ICollection<MaintenancePhoto>? MaintenancePhoto { get; set; }
    }
}
