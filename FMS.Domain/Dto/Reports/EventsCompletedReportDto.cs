using ClosedXML.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class EventsCompletedReportDto
    {
        public EventsCompletedReportDto() { }
        
        public EventsCompletedReportDto(EventReportDto reportDto)
        {
            HSIID = reportDto.FacilityNumber;
            FacilityName = reportDto.FacilityName;
            EventTypeName = reportDto.EventType?.Name;
            ActionTakenName = reportDto.ActionTaken?.Name;
            EventStartDate = reportDto.StartDate;
            EventCompletionDate = reportDto.CompletionDate;
            ComplianceOfficerName = reportDto.ComplianceOfficer?.Name;
            ActivityComment = reportDto.Comment;
        }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficerName { get; set; }

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

        [XLColumn(Header = "Comment")]
        public string ActivityComment { get; set; }
    }
}
