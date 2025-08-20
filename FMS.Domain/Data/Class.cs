using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> Classes => new List<string>
        {
            "I",
            "II",
            "III",
            "IV",
            "V",
            "ER"
        };
        public static string GetClass(string className)
        {
            return Classes.FirstOrDefault(c => c.Equals(className, StringComparison.OrdinalIgnoreCase)) ?? "Unknown";
        }
    }
}
