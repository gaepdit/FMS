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
            SourceProjected = status.SourceProjected;
            SoilStatusId = status.SoilStatusId;
            SoilDate = status.SoilDate;
            SoilProjected = status.SoilProjected;
            GroundwaterStatusId = status.GroundwaterStatusId;
            GroundwaterDate = status.GroundwaterDate;
            GroundwaterHWTF = status.GroundwaterHWTF;
            OverallStatusId = status.OverallStatusId;
            OverallDate = status.OverallDate;
            ISWQS = status.ISWQS;
            FundingSourceId = status.FundingSourceId;
            LandFill = status.LandFill;
            SolidWastePermitNumber = status.SolidWastePermitNumber;
            GAPSScore = status.GAPSScore;
            Comments = status.Comments;
            Lien = status.Lien;
            FinancialAssurance = status.FinancialAssurance;
        }

        public Guid FacilityId { get; set; }

        public Guid? SourceStatusId { get; set; }
        [Display(Name = "Source Status")]
        public SourceStatus SourceStatus { get; set; }

        [Display(Name = "Source Date")]
        public DateOnly? SourceDate { get; set; }

        [Display(Name = "Source Projected Cost")]
        public double? SourceProjected { get; set; }

        public Guid? SoilStatusId { get; set; }
        [Display(Name = "Soil Status")]
        public SoilStatus SoilStatus { get; set; }

        [Display(Name = "Soil Date")]
        public DateOnly? SoilDate { get; set; }

        [Display(Name = "Soil Projected Cost")]
        public double? SoilProjected { get; set; }

        public Guid? GroundwaterStatusId { get; set; }
        [Display(Name = "Groundwater Status")]
        public GroundwaterStatus GroundwaterStatus { get; set; }

        [Display(Name = "Groundwater Date")]
        public DateOnly? GroundwaterDate { get; set; }

        [Display(Name = "Groundwater HWTF Cost")]
        public double? GroundwaterHWTF { get; set; }

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

        [Display(Name = "GAPS Score")]
        public int GAPSScore { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Lien")]
        public bool Lien { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }
    }
}
