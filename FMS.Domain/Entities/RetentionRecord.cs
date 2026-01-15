using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class RetentionRecord : BaseActiveModel
    {
        public RetentionRecord() { }
        public RetentionRecord(Guid facilityId, RetentionRecordCreateDto create)
        {
            FacilityId = facilityId;
            StartYear = create.StartYear;
            EndYear = create.EndYear;
            BoxNumber = create.BoxNumber;
            ConsignmentNumber = create.ConsignmentNumber;
            ShelfNumber = create.ShelfNumber;
            RetentionSchedule = create.RetentionSchedule;
        }

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        // Starting Year (required)
        public int StartYear { get; set; }

        // Ending Year (required)
        public int EndYear { get; set; }

        // Corresponds to "Item" on Request Form (required)
        public string BoxNumber { get; set; }

        // Corresponds to "Accession Number" on RC spreadsheet
        public string ConsignmentNumber { get; set; }

        // Corresponds to "Location Number" on Request Form
        public string ShelfNumber { get; set; }

        // Retention Schedule Number DDDD-DDDD
        public string RetentionSchedule { get; set; }
    }
}
