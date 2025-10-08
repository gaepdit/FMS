using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class OnsiteScoreSummaryDto
    {
        public OnsiteScoreSummaryDto() { }

        public OnsiteScoreSummaryDto(OnsiteScore onsiteScore)
        {
            Id = onsiteScore.Id;
            Active = onsiteScore.Active;
            ScoreId = onsiteScore.FacilityId;
            OnsiteScoreValue = onsiteScore.OnsiteScoreValue;
            A = onsiteScore.A;
            B = onsiteScore.B;
            C = onsiteScore.C;
            Description = onsiteScore.Description;
            ChemName1D = onsiteScore.ChemName1D;
            Other1D = onsiteScore.Other1D;
            D2 = onsiteScore.D2;
            D3 = onsiteScore.D3;
            CASNO = onsiteScore.CASNO;
            E1 = onsiteScore.E1;
            E2 = onsiteScore.E2;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid ScoreId { get; set; }

        [Display(Name = "Onsite Score Value")]
        public decimal OnsiteScoreValue { get; set; }

        [Display(Name = "A")]
        public int? A { get; set; }

        [Display(Name = "B")]
        public int? B { get; set; }

        [Display(Name = "C")]
        public int? C { get; set; }

        [Display(Name = "On-Site Comment")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name 1D")]
        public string ChemName1D { get; set; }

        [Display(Name = "Other 1D")]
        public string Other1D { get; set; }

        [Display(Name = "D2")]
        public int? D2 { get; set; }

        [Display(Name = "D3")]
        public int? D3 { get; set; }

        public Guid ChemicalId { get; set; }
        [Display(Name = "Chemical")]
        public Chemical Chemical { get; set; }

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "E1")]
        public int? E1 { get; set; }

        [Display(Name = "E2")]
        public int? E2 { get; set; }
    }
}
