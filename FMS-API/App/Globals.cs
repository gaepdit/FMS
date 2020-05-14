using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API
{
    public static partial class FMS
    {
        // App-wide global variables

        // Server environment (value set in Startup.Configure)
        //internal static ServerEnvironment CurrentEnvironment { get; set; }

        // Date of final data migration from old FMS application 
        // into new EPD application: July 15, 2020
        public static readonly DateTime MigrationDate = new DateTime(2020, 7, 15);

        // Default pagination size for search results, etc.
        public const int PageSize = 25;

        // Limit on exporting search results to CSV
        public const int CsvRecordsExportLimit = 25000;

        // Image thumbnail size
        public const int ThumbnailSize = 90;
    }
}
