using FMS.Domain.Entities;
using System.Collections.Generic;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<PhoneType> PhoneTypes => new List<PhoneType>
        {
            new PhoneType { Id = 1 , Name = "Cell" },
            new PhoneType { Id = 2 , Name = "Work" },
            new PhoneType { Id = 3 , Name = "Fax" },
            new PhoneType { Id = 4 , Name = "Home" },
            new PhoneType { Id = 5 , Name = "Other" }
        };
    }
}
