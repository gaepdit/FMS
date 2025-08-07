using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class StatusSummaryDto
    {
        public StatusSummaryDto(Status status)
        {
            Id = status.Id;
            FacilityId = status.FacilityId;
            SourceStatus = status.SourceStatus;
            SourceDate = status.SourceDate;
            //SourceProjected = status.SourceProjected;
            SoilStatus = status.SoilStatus;
            SoilDate = status.SoilDate;
            //SoilProjected = status.SoilProjected;
            GroundwaterStatus = status.GroundwaterStatus;
            GroundwaterDate = status.GroundwaterDate;
            //GroundwaterHWTF = status.GroundwaterHWTF;
            OverallStatus = status.OverallStatus;
            OverallDate = status.OverallDate;
            ISWQS = status.ISWQS;
            FundingSource = status.FundingSource;
            LandFill = status.LandFill;
            SolidWastePermitNumber = status.SolidWastePermitNumber;
            GAPSScore = status.GAPSScore;
            Comments = status.Comments;
            Lien = status.Lien;
            FinancialAssurance = status.FinancialAssurance;
            GAPSModelDate = status.GAPSModelDate;
            GAPSNoOfUnknowns = status.GAPSNoOfUnknowns;
            GAPSAssessment = status.GAPSAssessment;
            CostEstimate = status.CostEstimate;
            CostEstimateDate = status.CostEstimateDate;
            AbandonSites = status.AbandonSites;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        public SourceStatus SourceStatus { get; set; }

        [Display(Name = "Source Date")]
        public DateOnly? SourceDate { get; set; }

        [Display(Name = "Source Projected")]
        public double? SourceProjected { get; set; }

        [Display(Name = "Soil Status")]
        public SoilStatus SoilStatus { get; set; }

        [Display(Name = "Soil Date")]
        public DateOnly? SoilDate { get; set; }

        [Display(Name = "Soil Projected")]
        public double? SoilProjected { get; set; }

        [Display(Name = "Groundwater Status")]
        public GroundwaterStatus GroundwaterStatus { get; set; }

        [Display(Name = "Groundwater Date")]
        public DateOnly? GroundwaterDate { get; set; }

        [Display(Name = "Groundwater HWTF")]
        public double? GroundwaterHWTF { get; set; }

        [Display(Name = "Overall Status")]
        public OverallStatus OverallStatus { get; set; }

        [Display(Name = "Overall Date")]
        public DateOnly? OverallDate { get; set; }

        [Display(Name = "ISWQS")]
        public bool ISWQS { get; set; }

        [Display(Name = "Funding Source")]
        public FundingSource FundingSource { get; set; }

        [Display(Name = "Land Fill")]
        public bool LandFill { get; set; }

        [Display(Name = "Solid Waste Permit Number")]
        public string SolidWastePermitNumber { get; set; }

        [Display(Name = "GAPS Score")]
        public int GAPSScore { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Lien")]
        public bool Lien { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }

        [Display(Name = "GAPS Model Date")]
        public DateOnly? GAPSModelDate { get; set; }

        [Display(Name = "GAPS No Of Unknowns")]
        public int GAPSNoOfUnknowns { get; set; }

        [Display(Name = "GAPS Assessment")]
        public GapsAssessment GAPSAssessment { get; set; }

        [Display(Name = "Cost Estimate")]
        public double? CostEstimate { get; set; }

        [Display(Name = "Cost Estimate Date")]
        public DateOnly? CostEstimateDate { get; set; }

        [Display(Name = "Abandon/Inactive")]
        public AbandonSites AbandonSites { get; set; }
    }
}
