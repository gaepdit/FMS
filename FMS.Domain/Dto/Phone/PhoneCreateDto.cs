using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PhoneCreateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active { get; set; } = true;

        public Guid ContactId { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "Phone Type")]
        public string PhoneType { get; set; }
    }
}
