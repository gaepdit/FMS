using ClosedXML.Attributes;
using System;

namespace FMS.Domain.Dto
{
    public class EventsPendingReportDto
    {
        public EventsPendingReportDto() {}

        public EventsPendingReportDto(EventReportDto reportEvent)
        {
            HSIID = reportEvent.FacilityNumber;
            FacilityName = reportEvent.FacilityName;
            EventTypeName = reportEvent.EventType?.Name;
            ActionTakenName = reportEvent.ActionTaken?.Name;
            EventStartDate = reportEvent.StartDate;
            EventDueDate = reportEvent.DueDate;
            DoneBy = reportEvent.DoneBy?.Name;
            UnitName = reportEvent.OrganizationalUnit?.Name;
            ActivityComment = reportEvent.Comment;
        }

        [XLColumn(Header = "Done By")]
        public string DoneBy { get; set; }

        [XLColumn(Header = "Unit")]
        public string UnitName { get; set; }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "Document Type")]
        public string EventTypeName { get; set; }

        [XLColumn(Header = "Event")]
        public string ActionTakenName { get; set; }

        [XLColumn(Header = "Activity Date")]
        public DateOnly? EventStartDate { get; set; }

        [XLColumn(Header = "Activity Due Date")]
        public DateOnly? EventDueDate { get; set; }

        [XLColumn(Header = "Activity Comment")]
        public string ActivityComment { get; set; }
    }
}
