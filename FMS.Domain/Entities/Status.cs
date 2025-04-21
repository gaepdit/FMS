using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class Status : BaseActiveModel
    {
        public Status() { }
        public Status(StatusCreateDto newStatus)
        {
            FacilityId = newStatus.FacilityId;
            SourceStatus = newStatus.SourceStatus;
            SourceDate = newStatus.SourceDate;
            SourceProjected = newStatus.SourceProjected;
            SoilStatus = newStatus.SoilStatus;
            SoilDate = newStatus.SoilDate;
            SoilProjected = newStatus.SoilProjected;
            GWStatus = newStatus.GWStatus;
            GWDate = newStatus.GWDate;
            GWHWTF = newStatus.GWHWTF;
            OverallStatus = newStatus.OverallStatus;
            OverallDate = newStatus.OverallDate;
            ISWQS = newStatus.ISWQS;
            PrimaryFundingSource = newStatus.PrimaryFundingSource;
            LandFill = newStatus.LandFill;
            SolidWastePermitNumber = newStatus.SolidWastePermitNumber;
            HSPMScore = newStatus.HSPMScore;
            Comments = newStatus.Comments;
            Lien = newStatus.Lien;
            FinancialAssurance = newStatus.FinancialAssurance;
        }
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
