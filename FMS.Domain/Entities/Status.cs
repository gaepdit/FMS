using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class Status : BaseActiveModel
    {
        public Status() { }
        public Status(StatusCreateDto newStatus)
        {
            Name = newStatus.Name;
            Description = newStatus.Description;
            IsRetained = newStatus.IsRetained;
        }
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
