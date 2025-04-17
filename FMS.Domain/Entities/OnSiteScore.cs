using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string Description { get; set; }

        public string ChemName1D { get; set; }

        public string Other1D { get; set; }

        public string D2 { get; set; }

        public string D3 { get; set; }

        public string CASNO { get; set; }

        public string E1 { get; set; }

        public string E2 { get; set; }
    }
}
