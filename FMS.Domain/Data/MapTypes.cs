using FMS.Domain.Entities;
using System.Collections.Generic;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> MapTypes => new List<string>
        {
            "roadmap",
            "terrain",
            "satellite",
            "hybrid",
        };
    }
}
