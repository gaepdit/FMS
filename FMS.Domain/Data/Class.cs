using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<Location> Classes => new List<Location>()
        {
            new() { Class = "I", Name = "The Director has determined that this site requires corrective action." },
            new() { Class = "II", Name = "Pending" },
            new() { Class = "III", Name = "The Director has determined that this site requires corrective action." },
            new() { Class = "IV", Name = "The Director has determined that this site requires corrective action." },
            new() { Class = "V", Name = "The Director has determined that this site requires corrective action." },
            new() { Class = "ER", Name = "Error" }
        };
        
    }
}
