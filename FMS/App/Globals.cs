using System;

namespace FMS.App
{
    // App-wide global variables
    public static class Globals
    {
        // Date of final data migration from old FMS application 
        // into new EPD application: July 15, 2020
        public static readonly DateTime MigrationDate = new DateTime(2020, 7, 15);

        // Default pagination size for search results, etc.
        public const int PageSize = 25;
    }
}
