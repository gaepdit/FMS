namespace FMS.Models.Models
{
    public class RetentionRecord : BaseActiveModel
    {
        public Facility Facility { get; set; }

        public int StartYear { get; set; }

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
