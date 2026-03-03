using ClosedXML.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto.Reports
{
    public class AbndInacStatusTrackerDto
    {
        public AbndInacStatusTrackerDto() { }

        public AbndInacStatusTrackerDto(AbndInacStatusTrackerDto abndInac)
        {
            HSINumber = abndInac.HSINumber;
            FacilityName = abndInac.FacilityName;
            City = abndInac.City;
            County = abndInac.County;
            AbndInac = abndInac.AbndInac;
            GAPSModelDate = abndInac.GAPSModelDate;
            GAPSScore = abndInac.GAPSScore;
            GAPSNoOfUnknowns = abndInac.GAPSNoOfUnknowns;
            GAPSAssessment = abndInac.GAPSAssessment;
            CostEstimate = abndInac.CostEstimate;
            AbndInacInfo = abndInac.AbndInacInfo;
            UnitName = abndInac.UnitName;
            EventComments = abndInac.EventComments;
            COName = abndInac.COName;
            GWScore = abndInac.GWScore;
            OnSiteScore = abndInac.OnSiteScore;
        }

        [XLColumn(Header = "HSI ID")]
        [Display(Name = "HSI ID")]
        public string HSINumber { get; set; }

        [XLColumn(Header = "Facility Name")]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [XLColumn(Header = "County")]
        [Display(Name = "County")]
        public string County { get; set; }

        [XLColumn(Header = "Abnd/Inac")]
        [Display(Name = "Abnd/Inac")]
        public string AbndInac { get; set; }

        [XLColumn(Header = "GAPS Model Date")]
        [Display(Name = "GAPS Model Date")]
        public DateOnly? GAPSModelDate { get; set; }

        [XLColumn(Header = "GAPS Score")]
        [Display(Name = "GAPS Score")]
        public int? GAPSScore { get; set; }

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

        [XLColumn(Header = "Pertinent Information")]
        [Display(Name = "Pertinent Information")]
        public string AbndInacInfo { get; set; }

        [XLColumn(Header = "Unit Name")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [XLColumn(Header = "Abnd/Inac Comment from Status tab")]
        [Display(Name = "Abnd/Inac Comment from Status tab")]
        public string EventComments { get; set; }

        [XLColumn(Header = "RQSM 1:C.O.")]
        [Display(Name = "RQSM 1: C.O.")]
        public string COName { get; set; }

        [XLColumn(Header = "RQSM 2: GW")]
        [Display(Name = "RQSM 2: GW")]
        public decimal? GWScore { get; set; }

        [XLColumn(Header = "RQSM 3: Onsite")]
        [Display(Name = "RQSM 3: Onsite")]
        public decimal? OnSiteScore { get; set; }
    }
}
