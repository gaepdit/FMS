using ClosedXML.Attributes;

namespace FMS.Domain.Dto.Reports
{
    public enum HSISortBy
    {
        HSINumber,
        Name,
        County,
        ClassName
    }
       
    public class HSIListReportDto
    {
        public HSIListReportDto() { }

        [XLColumn(Header ="HSI Number")]
        public string HSINumber { get; set; }

        [XLColumn(Header ="Facility Name")]
        public string Name { get; set; }

        [XLColumn(Header ="County")]
        public string County { get; set; }

        [XLColumn(Header ="Class")]
        public string ClassName { get; set; }

        [XLColumn(Header ="Facility Status")]
        public string FacilityStatus { get; set; }
    }
}
