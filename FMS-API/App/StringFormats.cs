using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API
{
    public static partial class FMS
    {
        // DateTime display formats
        public const string FormatDateTimeDisplay = "{0:MMMM\u00a0d, yyyy, h:mm\u00a0tt}";
        public const string FormatDateTimeShortDisplay = "{0:g}";
        public const string FormatDateDisplay = "{0:MMMM\u00a0d, yyyy}";
        public const string FormatDateShortDisplay = "{0:d}";

        // DateTime edit formats
        public const string FormatDateEdit = "{0:M-d-yyyy}";
        public const string FormatTimeEdit = "{0:h:mm\u00a0tt}";
    }
}
