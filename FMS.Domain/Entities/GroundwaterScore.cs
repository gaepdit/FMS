using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;

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
        public string A { get; set; }
        public string B1 { get; set; }
        public string B2 { get; set; }
        public string C { get; set; }
        public string Description { get; set; }
        public string ChemName { get; set; }
        public string Other { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string CASNO { get; set; }
        public string E1 { get; set; }
        public string E2 { get; set; }
    }
}
