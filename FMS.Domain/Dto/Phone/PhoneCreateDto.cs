using System;
using System.ComponentModel.DataAnnotations;

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
