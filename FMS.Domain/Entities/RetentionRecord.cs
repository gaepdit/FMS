using FMS.Domain.Entities.Base;
using System;

namespace FMS.Domain.Entities
{
    public class RetentionRecord : BaseActiveModel
    {
        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        // Starting Year
        public int StartYear { get; set; }

        // Ending Year
        public int EndYear { get; set; }

        // Corresponds to "Accession Number" on RC spreadsheet
        public string ConsignmentNumber { get; set; }

        // Corresponds to "Item" on Request Form
        public string BoxNumber { get; set; }

        // Corresponds to "Location Number" on Request Form
        public string ShelfNumber { get; set; }

        // Retention Schedule Number DDDD-DDDD
        public string RetentionSchedule { get; set; }
    }
}
