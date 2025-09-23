using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class GroundwaterScoreSummaryDto
    {
        public GroundwaterScoreSummaryDto(GroundwaterScore groundwaterScore)
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
            ChemName = groundwaterScore.ChemName;
            Other = groundwaterScore.Other;
            D2 = groundwaterScore.D2;
            D3 = groundwaterScore.D3;
            ChemicalId = groundwaterScore.ChemicalId;
            CASNO = groundwaterScore.CASNO;
            E1 = groundwaterScore.E1;
            E2 = groundwaterScore.E2;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Groundwater Score")]
        public string GWScore { get; set; }

        [Display(Name = "A")]
        public int? A { get; set; }

        [Display(Name = "B1")]
        public int? B1 { get; set; }

        [Display(Name = "B2")]
        public int? B2 { get; set; }

        [Display(Name = "C")]
        public int? C { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemName { get; set; }

        [Display(Name = "Other")]
        public string Other { get; set; }

        [Display(Name = "D2")]
        public int? D2 { get; set; }

        [Display(Name = "D3")]
        public int? D3 { get; set; }

        public Guid? ChemicalId { get; set; }

        [Display(Name = "Chemical")]
        public Chemical Chemical { get; set; }

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "E1")]
        public int? E1 { get; set; }

        [Display(Name = "E2")]
        public int? E2 { get; set; }
    }
}
