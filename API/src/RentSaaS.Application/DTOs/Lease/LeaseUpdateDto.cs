using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentSaaS.Application.DTOs.Lease
{
    public class LeaseUpdateDto : LeaseCreateDto
    {
        public Guid Id { get; set; }

    }
}
