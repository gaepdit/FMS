using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class PhoneCreateDto
    {
        public Guid ContactId { get; set; }

        [Display(Name = "Country Code")]
        public int CountryCode { get; set; }

        [Display(Name = "Area Code")]
        public int AreaCode { get; set; }

        [Display(Name = "Prefix")]
        public int Prefix { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }

        [Display(Name = "Phone Type")]
        public string Type { get; set; }
    }
}
