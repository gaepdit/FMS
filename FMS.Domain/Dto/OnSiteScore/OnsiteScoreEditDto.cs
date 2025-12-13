using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class OnsiteScoreEditDto
    {
        public OnsiteScoreEditDto() { }

        public OnsiteScoreEditDto(OnsiteScore onsiteScore)
        {
            Id = onsiteScore.Id;
            Active = onsiteScore.Active;
            FacilityId = onsiteScore.FacilityId;
            OnsiteScoreValue = onsiteScore.OnsiteScoreValue;
            A = onsiteScore.A;
            B = onsiteScore.B;
            C = onsiteScore.C;
            Description = onsiteScore.Description;
            ChemName1D = onsiteScore.Substance?.Chemical.ChemicalName;
            SubstanceId = onsiteScore.SubstanceId;
            Substance = onsiteScore.Substance;
            Other1D = onsiteScore.Other1D;
            D2 = onsiteScore.D2;
            D3 = onsiteScore.D3;
            CASNO = onsiteScore.Substance?.Chemical.CasNo;
            E1 = onsiteScore.E1;
            E2 = onsiteScore.E2;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Onsite Score Value")]
        public decimal OnsiteScoreValue { get; set; }

        [Display(Name = "A Access to Site")]
        public int? A { get; set; }

        [Display(Name = "B Release Type")]
        public int? B { get; set; }

        [Display(Name = "C Containment")]
        public int? C { get; set; }

        [Display(Name = "On-Site Comment")]
        public string Description { get; set; }

        [Display(Name = "Chemical Name 1D")]
        public string ChemName1D { get; set; }

        [Display(Name = "Other 1D")]
        public string Other1D { get; set; }

        [Display(Name = "D2 Tox Val")]
        public int? D2 { get; set; }

        [Display(Name = "D3 Quantity")]
        public int? D3 { get; set; }

        [Display(Name = "Substance")]
        public Guid? SubstanceId { get; set; }
        public Substance Substance { get; set; }

        [Display(Name = "CasNo")]
        public string CASNO { get; set; }

        [Display(Name = "1E Distance to Residence")]
        public int? E1 { get; set; }

        [Display(Name = "2E Sensitive Environment")]
        public int? E2 { get; set; }
    }
}
