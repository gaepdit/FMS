using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OnsiteScoreCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "On-Site Score")]
        public decimal OnsiteScoreValue { get; set; }

        [Display(Name = "A Access to Site")]
        public int? A { get; set; }

        [Display(Name = "B Release Type")]
        public int? B { get; set; }

        [Display(Name = "C Containment")]
        public int? C { get; set; }

        [Display(Name = "1D Chemical Name")]
        public string ChemName1D { get; set; }

        [Display(Name = "1D Other")]
        public string Other1D { get; set; }

        [Display(Name = "D2 Tox Val")]
        public int? D2 { get; set; }

        [Display(Name = "D3 Quantity")]
        public int? D3 { get; set; }

        [Display(Name = "On-Site Comment")]
        public string Description { get; set; }

        [Display(Name = "CAS Number")]
        public string CASNO { get; set; }

        [Display(Name = "1E Distance to Residence")]
        public int? E1 { get; set; }

        [Display(Name = "2E Sensitive Environment")]
        public int? E2 { get; set; }
    }
}