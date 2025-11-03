using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> ContactStatuses => new List<string>
        {
            "Primary",
            "Secondary",
            "Inactive",
            "Unknown"
        };
        public static string GetContactStatus(string contactStatus)
        {
            return ContactStatuses.FirstOrDefault(x => x.Equals(contactStatus, StringComparison.OrdinalIgnoreCase)) ?? "Unknown";
        }
    }
}
