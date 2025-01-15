using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Application.DTOs.Property
{
    public class PropertyCreateDto: BaseEntityDto
    {

        public string Address { get; set; }
        public string? Unite { get; set; }


    }
}
