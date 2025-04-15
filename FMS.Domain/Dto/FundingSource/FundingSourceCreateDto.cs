using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FundingSourceCreateDto
    {
        [Display(Name = "Funding Source")]
        [Required(ErrorMessage = "Funding Source Name is required.")]
        public string Name { get; set; }
        
    }
}
