using ClosedXML.Attributes;
using FMS.Domain.Dto;

namespace FMS.Helpers
{
    public class EventSummaryDtoScalar
    {
        public EventSummaryDtoScalar(EventSummaryDto dto, string facilityName, string facilityNumber)
        {   
            FacilityNumber = facilityNumber;
            FacilityName = facilityName;
            EventType = dto.EventType?.Name;
            ActionTaken = dto.ActionTaken?.Name;
            StartDate = dto.StartDate;
            DueDate = dto.DueDate;
            CompletionDate = dto.CompletionDate;
            ComplianceOfficer = dto.ComplianceOfficer?.Name;
            EventAmount = dto.EventAmount;
            EventContractor = dto.EventContractor?.Name;
            Comment = dto.Comment;
        }
        [XLColumn(Header = "Facility Number")]
        public string FacilityNumber { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "Event Type")]
        public string EventType { get; set; }

        [XLColumn(Header = "Action Taken")]
        public string ActionTaken { get; set; }

        [XLColumn(Header = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [XLColumn(Header = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [XLColumn(Header = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficer { get; set; }

        [XLColumn(Header = "Event Amount")]
        public decimal? EventAmount { get; set; }

        [XLColumn(Header = "Contractor")]
        public string EventContractor { get; set; }

        [XLColumn(Header = "Comment")]
        public string Comment { get; set; }
    }
}
