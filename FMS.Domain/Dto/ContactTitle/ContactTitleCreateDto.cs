using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactTitleCreateDto
    {
        [Display(Name = "Contact Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
