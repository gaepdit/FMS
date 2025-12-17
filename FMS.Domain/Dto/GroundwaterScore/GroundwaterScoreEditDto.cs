using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class GroundwaterScoreEditDto
    {
        public GroundwaterScoreEditDto() { }

        public GroundwaterScoreEditDto(GroundwaterScore groundwaterScore)
        {
            Id = groundwaterScore.Id;
            Active = groundwaterScore.Active;
            FacilityId = groundwaterScore.FacilityId;
            GWScore = groundwaterScore.GWScore;
            A = groundwaterScore.A;
            B1 = groundwaterScore.B1;
            B2 = groundwaterScore.B2;
            C = groundwaterScore.C;
            Description = groundwaterScore.Description;
            ChemName = groundwaterScore.Substance?.Chemical.ChemicalName;
            Other = groundwaterScore.Substance?.Chemical.CommonName;
            D2 = groundwaterScore.D2;
            D3 = groundwaterScore.D3;
            SubstanceId = groundwaterScore?.SubstanceId;
            Substance = groundwaterScore?.Substance;
            CASNO = groundwaterScore.Substance?.Chemical?.CasNo;
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

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

        [Display(Name = "GW Comment")]
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
    }
}
