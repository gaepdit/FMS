using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class RetentionRecord : BaseActiveModel
    {
        public Facility Facility { get; set; }

        [Display(Name = "Starting Year")]
        public int StartYear { get; set; }

        [Display(Name = "Ending Year")]
        public int EndYear { get; set; }

        // Corresponds to "Accession Number" on RC spreadsheet
        [Display(Name = "Consignment Number (Accession Number)")]
        public string ConsignmentNumber { get; set; }

        // Corresponds to "Item" on Request Form
        [Display(Name = "Box Number (Item)")]
        public string BoxNumber { get; set; }

        // Corresponds to "Location Number" on Request Form
        [Display(Name = "Shelf Number (Location Number)")]
        public string ShelfNumber { get; set; }

        // Retention Schedule Number DDDD-DDDD
        [Display(Name = "Retention Schedule Number")]
        public string RetentionSchedule { get; set; }

        public override string ToString()
        {
            return BoxNumber;
        }
    }
}
