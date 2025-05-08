using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GroundwaterScoreCreateDto
    {
        [Required]
        public Guid ScoreId { get; set; }

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

        [Display(Name = "CAS Number")]
        public string CASNO { get; set; }

        [Display(Name = "1E")]
        public int E1 { get; set; }

        [Display(Name = "2E")]
        public int E2 { get; set; }
    }
}
