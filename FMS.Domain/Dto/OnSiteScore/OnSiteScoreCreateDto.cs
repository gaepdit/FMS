using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OnSiteScoreCreateDto
    {
        [Required]
        public Guid ScoreId { get; set; }

        [Display(Name = "On-Site Score")]
        public string OnsiteScoreValue { get; set; }

        [Display(Name = "A")]
        public int A { get; set; }

        [Display(Name = "1B")]
        public int B { get; set; }

        [Display(Name = "C")]
        public int C { get; set; }

        [Display(Name = "1D Chemical Name")]
        public string ChemName1D { get; set; }

        [Display(Name = "1D Other")]
        public string Other1D { get; set; }

        [Display(Name = "2D")]
        public int D2 { get; set; }

        [Display(Name = "3D")]
        public int D3 { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public Guid ChemicalId { get; set; }

        [Display(Name = "CAS Number")]
        public string CASNO { get; set; }

        [Display(Name = "1E")]
        public int E1 { get; set; }

        [Display(Name = "2E")]
        public int E2 { get; set; }
    }
}