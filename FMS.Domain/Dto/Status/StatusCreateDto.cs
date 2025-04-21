using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class StatusCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public string SourceStatus { get; set; }

        public DateOnly? SourceDate { get; set; }

        public string SourceProjected { get; set; }

        public string SoilStatus { get; set; }

        public DateOnly? SoilDate { get; set; }

        public string SoilProjected { get; set; }

        public string GWStatus { get; set; }

        public DateOnly? GWDate { get; set; }

        public string GWHWTF { get; set; }

        public string OverallStatus { get; set; }

        public DateOnly? OverallDate { get; set; }

        public string ISWQS { get; set; }

        public string PrimaryFundingSource { get; set; }

        public bool LandFill { get; set; }

        public string SolidWastePermitNumber { get; set; }

        public int HSPMScore { get; set; }

        public string Comments { get; set; }

        public bool Lien { get; set; }

        public bool FinancialAssurance { get; set; }
    }
}
