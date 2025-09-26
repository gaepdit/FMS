using System;
using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Status : BaseActiveModel
    {
        public Status() { }

        public Status(Guid facilityId)
        {
            Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

        public Status(StatusCreateDto status)
        {
            FacilityId = status.FacilityId;
            SourceStatusId = status.SourceStatusId;
            SourceDate = status.SourceDate;
            SoilStatusId = status.SoilStatusId;
            SoilDate = status.SoilDate;
            GroundwaterStatusId = status.GroundwaterStatusId;
            GroundwaterDate = status.GroundwaterDate;
            OverallStatusId = status.OverallStatusId;
            OverallDate = status.OverallDate;
            ISWQS = status.ISWQS;
            FundingSourceId = status.FundingSourceId;
            LandFill = status.LandFill;
            SolidWastePermitNumber = status.SolidWastePermitNumber;
            GAPSScore = status.GAPSScore;
            GAPSModelDate = status.GAPSModelDate;
            GAPSNoOfUnknowns = status.GAPSNoOfUnknowns;
            GAPSAssessmentId = status.GAPSAssessmentId;
            Comments = status.Comments;
            Lien = status.Lien;
            FinancialAssurance = status.FinancialAssurance;
            CostEstimate = status.CostEstimate;
            CostEstimateDate = status.CostEstimateDate;
            AbandonSitesId = status.AbandonSitesId;
        }

        public Guid FacilityId { get; set; }

        public Guid? SourceStatusId { get; set; }
        [Display(Name = "Source Status")]
        public SourceStatus SourceStatus { get; set; }

        [Display(Name = "Source Date")]
        public DateOnly? SourceDate { get; set; }

        public Guid? SoilStatusId { get; set; }
        [Display(Name = "Soil Status")]
        public SoilStatus SoilStatus { get; set; }

        [Display(Name = "Soil Date")]
        public DateOnly? SoilDate { get; set; }

        public Guid? GroundwaterStatusId { get; set; }
        [Display(Name = "Groundwater Status")]
        public GroundwaterStatus GroundwaterStatus { get; set; }

        [Display(Name = "Groundwater Date")]
        public DateOnly? GroundwaterDate { get; set; }

        public Guid? OverallStatusId { get; set; }
        [Display(Name = "Overall Status")]
        public OverallStatus OverallStatus { get; set; }

        [Display(Name = "Overall Date")]
        public DateOnly? OverallDate { get; set; }

        [Display(Name = "ISWQS")]
        public bool ISWQS { get; set; }

        public Guid? FundingSourceId { get; set; }
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

        [Display(Name = "GAPS Model No. of Unknowns")]
        public int? GAPSNoOfUnknowns { get; set; }

        public Guid? GAPSAssessmentId { get; set; }
        [Display(Name = "GAPS Assessment")]
        public GapsAssessment GAPSAssessment { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Lien")]
        public bool Lien { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }

        [Display(Name = "Cost Estimate")]
        public double? CostEstimate { get; set; }

        [Display(Name = "Cost Estimate Date")]
        public DateOnly? CostEstimateDate { get; set; }

        public Guid? AbandonSitesId { get; set; }
        [Display(Name = "Abandon Sites")]
        public AbandonSites AbandonSites { get; set; }
    }
}
