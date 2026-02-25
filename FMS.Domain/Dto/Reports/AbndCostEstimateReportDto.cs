using ClosedXML.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto.Reports
{
    public class AbndCostEstimateReportDto
    {
        public AbndCostEstimateReportDto() { }

        [XLColumn(Header = "Class")]
        [Display(Name = "Class")]
        public string ClassName { get; set; }

        [XLColumn(Header = "HSI ID")]
        [Display(Name = "HSI ID")]
        public string HSINumber { get; set; }
        
        [XLColumn(Header = "Facility Name")]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }
        
        [XLColumn(Header = "County")]
        [Display(Name = "County")]
        public string County { get; set; }

        [XLColumn(Header = "C.O.")]
        [Display(Name = "C.O.")]
        public string COName { get; set; }

        [XLColumn(Header = "GAPS Score")]
        [Display(Name = "GAPS Score")]
        public int? GAPSScore { get; set; }

        [XLColumn(Header = "GAPS Model Date")]
        [Display(Name = "GAPS Model Date")]
        public DateOnly? GAPSModelDate { get; set; }

        [XLColumn(Header = "GAPS No Of Unknowns")]
        [Display(Name = "GAPS No Of Unknowns")]
        public int? GAPSNoOfUnknowns { get; set; }

        [XLColumn(Header = "GAPS Assessment")]
        [Display(Name = "GAPS Assessment")]
        public string GAPSAssessment { get; set; }

        [XLColumn(Header = "Cost Estimate")]
        [Display(Name = "Cost Estimate")]
        [DataType(DataType.Currency)]
        public decimal? CostEstimate { get; set; }
    }
}
