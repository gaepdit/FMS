using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class StatusCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Source Status")]
        public Guid SourceStatusId { get; set; }
        [Display(Name = "Source Status")]
        public SourceStatus SourceStatus { get; set; }

        [Display(Name = "Source Date")]
        public DateOnly? SourceDate { get; set; }

        [Display(Name = "Soil Status")]
        public Guid SoilStatusId { get; set; }
        [Display(Name = "Soil Status")]
        public SoilStatus SoilStatus { get; set; }

        [Display(Name = "Soil Date")]
        public DateOnly? SoilDate { get; set; }

        [Display(Name = "Groundwater Status")]
        public Guid GroundwaterStatusId { get; set; }
        [Display(Name = "Groundwater Status")]
        public GroundwaterStatus GroundwaterStatus { get; set; }

        [Display(Name = "Groundwater Date")]
        public DateOnly? GroundwaterDate { get; set; }

        [Display(Name = "Overall Status")]
        public Guid OverallStatusId { get; set; }
        [Display(Name = "Overall Status")]
        public OverallStatus OverallStatus { get; set; }

        [Display(Name = "Overall Date")]
        public DateOnly? OverallDate { get; set; }

        [Display(Name = "ISWQS")]
        public bool ISWQS { get; set; }

        [Display(Name = "Funding Source")]
        public Guid FundingSourceId { get; set; }
        [Display(Name = "Funding Source")]
        public FundingSource FundingSource { get; set; }

        [Display(Name = "Land Fill")]
        public bool LandFill { get; set; }

        [Display(Name = "Solid Waste Permit Number")]
        public string SolidWastePermitNumber { get; set; }

        [Display(Name = "GAPS Model Score")]
        public int? GAPSScore { get; set; }

        [Display(Name = "GAPS Model Date")]
        public DateOnly? GAPSModelDate { get; set; }

        [Display(Name = "GAPS No. of Unknowns")]
        public int? GAPSNoOfUnknowns { get; set; }

        [Display(Name = "GAPS Assessment")]
        public Guid? GAPSAssessmentId { get; set; }
        [Display(Name = "GAPS Assessment")]
        public GapsAssessment GAPSAssessment { get; set; }

        [Display(Name = "General Status Comments (not used in reporting)")]
        public string Comments { get; set; }

        [Display(Name = "Lien")]
        public bool Lien { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }

        [Display(Name = "Cost Estimate from 5-Yr Review")]
        [DataType(DataType.Currency)]
        public decimal? CostEstimate { get; set; }

        [Display(Name = "Cost Estimate Date")]
        public DateOnly? CostEstimateDate { get; set; }

        [Display(Name = "Pertinent Information for Aban/Inac sites")]
        public Guid? AbandonedInactiveId { get; set; }
        [Display(Name = "Abandoned/Inactive")]
        public AbandonedInactive AbandonedInactive { get; set; }

        [Display(Name = "Comments for Aban/Inac Status Tracker Report")]
        public string ReportComments { get; set; }
    }
}
