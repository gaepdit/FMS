using ClosedXML.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PAFReportDto
    {
        public PAFReportDto() { }

        public PAFReportDto(PAFReportRawDto raw)
        {
            HSIId = raw.HSIId;
            SiteName = raw.SiteName;
            PAFIssueDate = raw.PAFIssueDate.HasValue ? DateOnly.FromDateTime(raw.PAFIssueDate.Value) : null;
            PAFAmount = raw.PAFAmount;
            ProjectOfficer = raw.ProjectOfficer;
            Contractor = raw.Contractor;
            RAWReceived = raw.RAWReceived.HasValue ? DateOnly.FromDateTime(raw.RAWReceived.Value) : null;
            RAWDue = raw.RAWDue.HasValue ? DateOnly.FromDateTime(raw.RAWDue.Value) : null;
            RAWApproved = raw.RAWApproved.HasValue ? DateOnly.FromDateTime(raw.RAWApproved.Value) : null;
            RARReceived = raw.RARReceived.HasValue ? DateOnly.FromDateTime(raw.RARReceived.Value) : null;
            RARDue = raw.RARDue.HasValue ? DateOnly.FromDateTime(raw.RARDue.Value) : null;
            RARApproved = raw.RARApproved.HasValue ? DateOnly.FromDateTime(raw.RARApproved.Value) : null;
            ProjectCompleteDue = raw.ProjectCompleteDue.HasValue ? DateOnly.FromDateTime(raw.ProjectCompleteDue.Value) : null;
            ProjectCompleteActual = raw.ProjectCompleteActual.HasValue ? DateOnly.FromDateTime(raw.ProjectCompleteActual.Value) : null;
            ProjectComments = raw.ProjectComments;
        }

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