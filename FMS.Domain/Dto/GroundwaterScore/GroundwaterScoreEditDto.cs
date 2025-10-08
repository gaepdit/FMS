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
            ChemName = groundwaterScore.Chemical?.ChemicalName;
            Other = groundwaterScore.Chemical?.CommonName;
            D2 = groundwaterScore.D2;
            D3 = groundwaterScore.D3;
            ChemicalId = groundwaterScore?.ChemicalId;
            Chemical = groundwaterScore?.Chemical;
            CASNO = groundwaterScore.Chemical?.CasNo;
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Groundwater Score")]
        public decimal GWScore { get; set; }

        [Display(Name = "A")]
        public int? A { get; set; }

        [Display(Name = "1B")]
        public int? B1 { get; set; }

        [Display(Name = "2B")]
        public int? B2 { get; set; }

        [Display(Name = "C")]
        public int? C { get; set; }

        [Display(Name = "GW Comment")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemName { get; set; }

        [Display(Name = "Other")]
        public string Other { get; set; }

        [Display(Name = "2D")]
        public int? D2 { get; set; }

        [Display(Name = "3D")]
        public int? D3 { get; set; }

        [Display(Name = "Chemical")]
        public Guid? ChemicalId { get; set; }
        public Chemical Chemical { get; set; }

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "1E")]
        public int? E1 { get; set; }

        [Display(Name = "2E")]
        public int? E2 { get; set; }
    }
}
