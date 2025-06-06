using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PhoneCreateDto
    {
        public bool Active { get; set; } = true;

        public Guid ContactId { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "Phone Type")]
        public int PhoneTypeId { get; set; }
    }
}
