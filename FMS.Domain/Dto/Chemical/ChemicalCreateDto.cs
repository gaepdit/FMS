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

        [Display(Name = "Toxicity Value")]
        public string ToxValue { get; set; }

        [Display(Name = "MCL Value")]
        public string MCLs { get; set; }

        [Display(Name = "Final RC")]
        public string FinalRc { get; set; }

        [Display(Name = "RQ")]
        public string RQ { get; set; }

        public void TrimAll()
        {
            CasNo = CasNo?.Trim();
            ChemicalName = ChemicalName?.Trim();
            CommonName = CommonName?.Trim();
            ToxValue = ToxValue?.Trim();
            MCLs = MCLs?.Trim();
        }
    }
}
