using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class StatusCreateDto
    {
        // Name of the status
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        // Description of the status
        [StringLength(200)]
        public string Description { get; set; }
        // Indicates if the status is retained
        public bool IsRetained { get; set; }
    }
}
