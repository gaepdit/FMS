using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class OnsiteScore : BaseActiveModel
    {
        public OnsiteScore() { }

        public OnsiteScore(OnsiteScoreCreateDto onsiteScore)
        {
            FacilityId = onsiteScore.FacilityId;
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

        public OnsiteScore(OnsiteScore onsiteScore)
        {
            Id = onsiteScore.Id;
            Active = onsiteScore.Active;
            FacilityId = onsiteScore.FacilityId;
            OnsiteScoreValue = onsiteScore.OnsiteScoreValue;
            A = onsiteScore.A;
            B = onsiteScore.B;
            C = onsiteScore.C;
            Description = onsiteScore.Description;
            ChemName1D = onsiteScore.GetChemName();
            Other1D = onsiteScore.Other1D;
            D2 = onsiteScore.D2;
            D3 = onsiteScore.D3;
            ChemicalId = onsiteScore.ChemicalId;
            Chemical = onsiteScore.Chemical;
            CASNO = onsiteScore.GetCasNo();
            E1 = onsiteScore.E1;
            E2 = onsiteScore.E2;
        }

        public Guid FacilityId { get; set; }

        [Display(Name = "On-Site Score")]
        public string OnsiteScoreValue { get; set; }

        [Display(Name = "A")]
        public int A { get; set; }

        [Display(Name = "B")]
        public int B { get; set; }

        [Display(Name = "C")]
        public int C { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "1D Chemical Name")]
        public string ChemName1D { get; set; }

        [Display(Name = "1D Other")]
        public string Other1D { get; set; }

        [Display(Name = "2D")]
        public int D2 { get; set; }

        [Display(Name = "3D")]
        public int D3 { get; set; }

        public Guid ChemicalId { get; set; }
        [Display(Name = "Chemical")]
        public Chemical Chemical { get; set; }  

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "1E")]
        public int E1 { get; set; }

        [Display(Name = "2E")]
        public int E2 { get; set; }

        public string GetCasNo()
        {
            return Chemical?.CasNo ?? CASNO;
        }

        public string GetChemName()
        {
            return Chemical?.ChemicalName ?? ChemName1D;
        }
    }
}
