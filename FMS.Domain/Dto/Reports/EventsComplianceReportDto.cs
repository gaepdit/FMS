using ClosedXML.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventsComplianceReportDto
    {
        EventsComplianceReportDto() { }

        public EventsComplianceReportDto(EventReportDto reportDto)
        {
            HSIID = reportDto.FacilityNumber;
            FacilityName = reportDto.FacilityName;
            EventTypeName = reportDto.EventType?.Name;
            ActionTakenName = reportDto.ActionTaken?.Name;
            EventStartDate = reportDto.StartDate;
            EventCompletionDate = reportDto.CompletionDate;
            EventAmount = reportDto.EventAmount;
        }

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

        [XLColumn(Header = "Event Amount")]
        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }
    }
}
