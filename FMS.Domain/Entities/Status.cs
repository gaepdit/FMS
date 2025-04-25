using System;
using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class Status : BaseActiveModel
    {
        public Status() { }

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
            HSPMScore = status.HSPMScore;
            Comments = status.Comments;
            Lien = status.Lien;
            FinancialAssurance = status.FinancialAssurance;
        }

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

        public DateOnly? OverallDate { get; set; }  //Make Dropdown

        public string ISWQS { get; set; }

        public Guid FundingSourceId { get; set; }
        public FundingSource FundingSource { get; set; }

        public bool LandFill { get; set; }

        public string SolidWastePermitNumber { get; set; }

        public int HSPMScore { get; set; }

        public string Comments { get; set; }

        public bool Lien { get; set; }

        public bool FinancialAssurance { get; set; }
    }
}
