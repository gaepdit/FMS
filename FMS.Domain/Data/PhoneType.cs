using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> PhoneTypes => new List<string>
        {
            "Cell",
            "Work",
            "Fax",
            "Home",
            "Other"
        };
        public static string GetPhoneType(string phoneType)
        {
            return PhoneTypes.FirstOrDefault(x => x.Equals(phoneType, StringComparison.OrdinalIgnoreCase)) ?? "Other";
        }
    }
}
