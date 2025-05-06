using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ChemicalCreateDto
    {
        [Display(Name = "CASNO")]
        [Required(ErrorMessage = "CASNO is required.")]
        public string CasNo { get; set; }

        [Display(Name = "Chemical Name")]
        [Required(ErrorMessage = "Chemical Name is required.")]
        public string ChemicalName { get; set; }

        [Display(Name = "Common Name")]
        public string CommonName { get; set; }

        public string ToxValue { get; set; }

        public string MCLs { get; set; }
    }
}
