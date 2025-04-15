using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ToxValue { get; set; }

        public string MCLs { get; set; }
    }
}
