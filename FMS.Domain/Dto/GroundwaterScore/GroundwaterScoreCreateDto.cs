using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GroundwaterScoreCreateDto
    {
        [Required]
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

        public Guid? SubstanceId { get; set; }

        [Display(Name = "CAS Number")]
        public string CASNO { get; set; }

        [Display(Name = "1E Exposure")]
        public int? E1 { get; set; }

        [Display(Name = "2E Distance to Well")]
        public int? E2 { get; set; }
    }
}
