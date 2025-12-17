using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class GroundwaterScore : BaseActiveModel
    {
        public GroundwaterScore() { }

        public GroundwaterScore(Guid facilityId)
        {
            Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

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
            SubstanceId = groundwaterScore.SubstanceId;
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
            SubstanceId = groundwaterScore.SubstanceId;
            Substance = groundwaterScore.Substance;
            CASNO = groundwaterScore.GetCasNo();
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public Guid FacilityId { get; set; }

        [Display(Name = "Groundwater Score")]
        public decimal GWScore { get; set; }

        [Display(Name = "A Release Type")]
        public int? A { get; set; }

        [Display(Name = "1B Susceptibility")]
        public int? B1 { get; set; }

        [Display(Name = "2B Physical State")]
        public int? B2 { get; set; }

        [Display(Name = "C Containment")]
        public int? C { get; set; }

        [Display(Name = "GW Comments")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemName { get; set; }

        [Display(Name = "Other")]
        public string Other { get; set; }

        [Display(Name = "2D Tox Val")]
        public int? D2 { get; set; }

        [Display(Name = "3D Quantity")]
        public int? D3 { get; set; }

        [Display(Name = "Substance")]
        public Guid? SubstanceId { get; set; }
        public Substance Substance { get; set; }

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "1E Exposure")]
        public int? E1 { get; set; }

        [Display(Name = "2E Distance to Well")]
        public int? E2 { get; set; }

        public string GetCasNo()
        {
            return Substance?.Chemical.CasNo ?? CASNO;
        }

        public string GetChemName()
        {
            return Substance?.Chemical.ChemicalName ?? ChemName;
        }
    }
}
