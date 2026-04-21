using ClosedXML.Attributes;

namespace FMS.Domain.Dto.Reports
{
    public class EventsOutstandingReportDto
    {
        public EventsOutstandingReportDto() { }

        public EventsOutstandingReportDto(EventReportDto reportDto)
        {
            Unit = reportDto.OrganizationalUnit?.Name;
            HSIID = reportDto.FacilityNumber;
            FacilityName = reportDto.FacilityName;
            EventTypeName = reportDto.EventType?.Name;
            ActionTakenName = reportDto.ActionTaken?.Name;
            EventStartDate = reportDto.StartDate;
            EventDueDate = reportDto.DueDate;
            DoneBy = reportDto.DoneBy?.Name;
        }

        [XLColumn(Header = "Unit")]
        public string Unit { get; set; }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "Document Type")]
        public string EventTypeName { get; set; }

        [XLColumn(Header = "Event")]
        public string ActionTakenName { get; set; }

        [XLColumn(Header = "Start Date")]
        public DateOnly? EventStartDate { get; set; }

        [XLColumn(Header = "Due Date")]
        public DateOnly? EventDueDate { get; set; }

        [XLColumn(Header = "Done By")]
        public string DoneBy { get; set; }
    }
}
