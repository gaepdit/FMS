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

        [Required]
        public Guid ScoreId { get; set; }

        public string OnsiteScoreValue { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public string Description { get; set; }

        public string ChemName1D { get; set; }

        public string Other1D { get; set; }

        public int D2 { get; set; }

        public int D3 { get; set; }

        public Guid ChemicalId { get; set; }

        public string CASNO { get; set; }

        public int E1 { get; set; }

        public int E2 { get; set; }
    }
}
