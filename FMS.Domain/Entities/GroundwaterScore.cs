using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class GroundwaterScore : BaseActiveModel
    {
        public GroundwaterScore() { }

        public GroundwaterScore(GroundwaterScoreCreateDto groundwaterScore)
        {
            ScoreId = groundwaterScore.ScoreId;
            GWScore = groundwaterScore.GWScore;
            A = groundwaterScore.A;
            B1 = groundwaterScore.B1;
            B2 = groundwaterScore.B2;
            C = groundwaterScore.C;
            Description = groundwaterScore.Description;
            ChemName = groundwaterScore.ChemName;
            Other = groundwaterScore.Other;
            D2 = groundwaterScore.D2;
            D3 = groundwaterScore.D3;
            CASNO = groundwaterScore.CASNO;
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }
        public Guid ScoreId { get; set; }

        public string GWScore { get; set; }

        public int A { get; set; }

        public int B1 { get; set; }

        public int B2 { get; set; }

        public int C { get; set; }

        public string Description { get; set; }

        public string ChemName { get; set; }

        public string Other { get; set; }

        public int D2 { get; set; }

        public int D3 { get; set; }

        public Guid ChemicalId { get; set; }
        public string CASNO { get; set; }

        public int E1 { get; set; }

        public int E2 { get; set; }
    }
}
