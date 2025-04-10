using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> PhoneTypes =>
        [
            "Cell",
            "Work",
            "Fax",
            "Home",
            "Other"
        ];
        public static string GetPhoneType(string phoneType)
        {
            return PhoneTypes.FirstOrDefault(x => x.Equals(phoneType, StringComparison.OrdinalIgnoreCase)) ?? "Other";
        }
    }
}
