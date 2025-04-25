using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class StatusCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public Guid SourceStatusId { get; set; }
        public SourceStatus SourceStatus { get; set; }

        public DateOnly? SourceDate { get; set; }

        public string SourceProjected { get; set; }

        public Guid SoilStatusId { get; set; }
        public SoilStatus SoilStatus { get; set; }

        public DateOnly? SoilDate { get; set; }

        public string SoilProjected { get; set; }

        public Guid GroundwaterStatusId { get; set; }
        public GroundwaterStatus GroundwaterStatus { get; set; }

        public DateOnly? GroundwaterDate { get; set; }

        public string GroundwaterHWTF { get; set; }

        public Guid OverallStatusId { get; set; }
        public OverallStatus OverallStatus { get; set; }

        public DateOnly? OverallDate { get; set; }

        public string ISWQS { get; set; }

        public Guid PrimaryFundingSourceId { get; set; }
        public FundingSource PrimaryFundingSource { get; set; }

        public bool LandFill { get; set; }

        public string SolidWastePermitNumber { get; set; }

        public int HSPMScore { get; set; }

        public string Comments { get; set; }

        public bool Lien { get; set; }

        public bool FinancialAssurance { get; set; }
    }
}
