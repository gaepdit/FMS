using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PAFReportRawDto
    {
        public string HSIId { get; set; }

        public string SiteName { get; set; }

        public DateTime? PAFIssueDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal? PAFAmount { get; set; }

        public string ProjectOfficer { get; set; }

        public string Contractor { get; set; }

        public DateTime? RAWReceived { get; set; }

        public DateTime? RAWDue { get; set; }

        public DateTime? RAWApproved { get; set; }

        public DateTime? RARReceived { get; set; }

        public DateTime? RARDue { get; set; }

        public DateTime? RARApproved { get; set; }

        public DateTime? ProjectCompleteDue { get; set; }

        public DateTime? ProjectCompleteActual { get; set; }

        public string ProjectComments { get; set; }
    }
}
