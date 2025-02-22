using RentSaaS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Domain.Entities
{
    public class ExpenseFile : BaseEntity
    {
        [ForeignKey(nameof(Expense))]
        public Guid ExpenseId { get; set; }
        public string ?FileName { get; set; }

        public Expense ?Expense { get; set; }
        public DateTime UploadedAt { get; set; }
        public long FileSize { get; set; }
    }

}
