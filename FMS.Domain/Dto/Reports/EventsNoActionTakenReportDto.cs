using ClosedXML.Attributes;

namespace FMS.Domain.Dto
{
    public class EventsNoActionTakenReportDto
    {
        public EventsNoActionTakenReportDto() { }

        public EventsNoActionTakenReportDto(EventsNoActionTakenReportDto reportDto)
        {
            HSIID = reportDto.HSIID;
            FacilityName = reportDto.FacilityName;
            ListDate = reportDto.ListDate;
            ComplianceOfficerName = reportDto.ComplianceOfficerName;
        }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "List Date")]
        public DateOnly? ListDate { get; set; }

        [XLColumn(Header = "HSRA CO")]
        public string ComplianceOfficerName { get; set; }
    }
}
