using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class GroundwaterScore : BaseActiveModel
    {
        public GroundwaterScore() { }

        public GroundwaterScore(GroundwaterScoreCreateDto groundwaterScore)
        {
            FacilityId = groundwaterScore.FacilityId;
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
            ChemicalId = groundwaterScore.ChemicalId;
            CASNO = groundwaterScore.CASNO;
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public GroundwaterScore(GroundwaterScore groundwaterScore)
        {
            Id = groundwaterScore.Id;
            FacilityId = groundwaterScore.FacilityId;
            GWScore = groundwaterScore.GWScore;
            A = groundwaterScore.A;
            B1 = groundwaterScore.B1;
            B2 = groundwaterScore.B2;
            C = groundwaterScore.C;
            Description = groundwaterScore.Description;
            ChemName = groundwaterScore.GetChemName();
            Other = groundwaterScore.Other;
            D2 = groundwaterScore.D2;
            D3 = groundwaterScore.D3;
            ChemicalId = groundwaterScore.ChemicalId;
            Chemical = groundwaterScore.Chemical;
            CASNO = groundwaterScore.GetCasNo();
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public Guid FacilityId { get; set; }

        [Display(Name = "Groundwater Score")]
        public string GWScore { get; set; }

        [Display(Name = "A")]
        public int A { get; set; }

        [Display(Name = "1B")]
        public int B1 { get; set; }

        [Display(Name = "2B")]
        public int B2 { get; set; }

        [Display(Name = "C")]
        public int C { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemName { get; set; }

        [Display(Name = "Other")]
        public string Other { get; set; }

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
            return Chemical?.ChemicalName ?? ChemName;
        }
    }
}
