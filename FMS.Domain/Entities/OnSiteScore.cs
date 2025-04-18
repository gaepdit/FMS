using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class OnSiteScore : BaseActiveModel
    {
        public OnSiteScore() { }

        public OnSiteScore(OnSiteScoreCreateDto onSiteScore)
        {
            ScoreId = onSiteScore.ScoreId;
            ScoreValue = onSiteScore.ScoreValue;
            A = onSiteScore.A;
            B = onSiteScore.B;
            C = onSiteScore.C;
            Description = onSiteScore.Description;
            ChemName1D = onSiteScore.ChemName1D;
            Other1D = onSiteScore.Other1D;
            D2 = onSiteScore.D2;
            D3 = onSiteScore.D3;
            CASNO = onSiteScore.CASNO;
            E1 = onSiteScore.E1;
            E2 = onSiteScore.E2;
        }
        public Guid ScoreId { get; set; }

        public string ScoreValue { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public string Description { get; set; }

        public string ChemName1D { get; set; }

        public string Other1D { get; set; }

        public int D2 { get; set; }

        public int D3 { get; set; }

        public string CASNO { get; set; }

        public int E1 { get; set; }

        public int E2 { get; set; }
    }
}
