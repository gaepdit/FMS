using ClosedXML.Attributes;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class EventsPendingReportDto
    {
        public EventsPendingReportDto() { }

        public EventsPendingReportDto(EventSummaryDto reportEvent, string hsiid, string facilityName, string unitName)
        {
            HSIID = hsiid;
            FacilityName = facilityName;
            EventTypeName = reportEvent.EventType.Name;
            EventStartDate = reportEvent.StartDate;
            EventDueDate = reportEvent.DueDate;
            ComplianceOfficerName = reportEvent.ComplianceOfficer.Name;
            UnitName = unitName;
            ActivityComment = reportEvent.Comment;
        }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "Event Type")]
        public string EventTypeName { get; set; }

        [XLColumn(Header = "Event Start Date")]
        public DateOnly? EventStartDate { get; set; }

        [XLColumn(Header = "Event Due Date")]
        public DateOnly? EventDueDate { get; set; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficerName { get; set; }

        [XLColumn(Header = "Unit")]
        public string UnitName { get; set; }

        [XLColumn(Header = "Activity Comment")]
        public string ActivityComment { get; set; }
    }
}
