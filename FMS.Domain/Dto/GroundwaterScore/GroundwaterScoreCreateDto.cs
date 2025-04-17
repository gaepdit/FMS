using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class GroundwaterScoreCreateDto
    {
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
