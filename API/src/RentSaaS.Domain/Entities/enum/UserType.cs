using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RentSaaS.Domain.Entities
{
    public enum UserType
    {
        [EnumMember(Value = "landlord")]
        Landlord = 0,

        [EnumMember(Value = "tenant")]
        Tenant = 1
    }
}
