using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.RecordPaymentFile
{
    public class RecordPaymentUpdateDto: RecordPaymentCreateDto
    {
        public Guid Id { get; set; }

    }
}
