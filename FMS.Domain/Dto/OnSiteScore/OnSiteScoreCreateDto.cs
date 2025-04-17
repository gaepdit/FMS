using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class OnSiteScoreCreateDto
    {
        public Guid ScoreId { get; set; }
        public string OnSiteScore { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string ChemName1D { get; set; }
        public string Other1D { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string Description { get; set; }
        public string CASNO { get; set; }
        public string E1 { get; set; }
        public string E2 { get; set; }
    }
}
