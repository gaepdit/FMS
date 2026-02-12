using ClosedXML.Attributes;
using System;

namespace FMS.Domain.Dto
{
    public class EventsCompletedOutstandingReportDto
    {
        public EventsCompletedOutstandingReportDto() { }

        public EventsCompletedOutstandingReportDto(EventReportDto reportDto)
        {
           
            HSIID = reportDto.FacilityNumber;
            FacilityName = reportDto.FacilityName;
            FacilityTypeName = reportDto.FacilityType?.Name;
            EventTypeName = reportDto.EventType?.Name;
            ActionTakenName = reportDto.ActionTaken?.Name;
            EventStartDate = reportDto.StartDate;
            EventCompletionDate = reportDto.CompletionDate;
            Days = (reportDto.CompletionDate.HasValue && reportDto.StartDate.HasValue) 
                ? (reportDto.CompletionDate.Value.ToDateTime(TimeOnly.MinValue) - reportDto.StartDate.Value.ToDateTime(TimeOnly.MinValue)).TotalDays 
                : 0;
            DoneBy = reportDto.DoneBy?.Name;
            ActivityComment = reportDto.Comment;
        }

        [XLColumn(Header = "Facility Type")]
        public string FacilityTypeName { get; set; }

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

        [XLColumn(Header = "End Date")]
        public DateOnly? EventCompletionDate { get; set; }

        [XLColumn(Header = "Days")]
        public double Days { get; set; }

        [XLColumn(Header = "Done By")]
        public string DoneBy { get; set; }

        [XLColumn(Header = "Comment")]
        public string ActivityComment { get; set; }
    }
}
