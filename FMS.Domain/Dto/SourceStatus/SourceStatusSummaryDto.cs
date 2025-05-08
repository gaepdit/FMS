using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class SourceStatusSummaryDto
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Source Status")]
        [Required(ErrorMessage = "Source Status Name is required.")]
        public string Name { get; set; }
    }
}
