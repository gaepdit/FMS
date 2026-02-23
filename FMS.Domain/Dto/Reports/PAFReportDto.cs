using ClosedXML.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PAFReportDto
    {

        [Display(Name = "HSI ID")]
        [XLColumn(Header = "HSI ID")]
        public string HSIId { get; set; }

        [Display(Name = "Site Name")]
        [XLColumn(Header = "Site Name")]
        public string SiteName { get; set; }

        [Display(Name = "PAF Issue Date")]
        [XLColumn(Header = "PAF Issue Date")]

        public DateTime? PAFIssueDate { get; set; }


        [Display(Name = "PAF Amount")]
        [XLColumn(Header = "PAF Amount")]
        [DataType(DataType.Currency)]
        public decimal? PAFAmount { get; set; }

        [Display(Name = "Project Officer")]
        [XLColumn(Header = "Project Officer")]
        public string ProjectOfficer { get; set; }

        [Display(Name = "Contractor")]
        [XLColumn(Header = "Contractor")]
        public string Contractor { get; set; }

        [Display(Name = "RAW Received")]
        [XLColumn(Header = "RAW Received")]

        public DateTime? RAWReceived { get; set; }

        [Display(Name = "RAW Due")]
        [XLColumn(Header = "RAW Due")]
        public DateTime? RAWDue { get; set; }

        [Display(Name = "RAW Approved")]
        [XLColumn(Header = "RAW Approved")]
        public DateTime? RAWApproved { get; set; }

        [Display(Name = "RAR Received")]
        [XLColumn(Header = "RAR Received")]
        public DateTime? RARReceived { get; set; }

        [Display(Name = "RAR Due")]
        [XLColumn(Header = "RAR Due")]
        public DateTime? RARDue { get; set; }

        [Display(Name = "RAR Approved")]        
        [XLColumn(Header = "RAR Approved")]
        public DateTime? RARApproved { get; set; }

        [Display(Name = "Project Complete Due")]
        [XLColumn(Header = "Project Complete Due")]
        public DateTime? ProjectCompleteDue { get; set; }

        [Display(Name = "Project Complete Actual")]
        [XLColumn(Header = "Project Complete Actual")]
        public DateTime? ProjectCompleteActual { get; set; }


        [Display(Name = "Project Comments")]
        [XLColumn(Header = "Project Comments")]
        public string ProjectComments { get; set; }
    }
}