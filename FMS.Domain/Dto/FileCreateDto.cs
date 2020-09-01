using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class FileCreateDto
    {
        [StringLength(9)]
        [Display(Name = "File Label"), Required]
        public string FileLabel { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
