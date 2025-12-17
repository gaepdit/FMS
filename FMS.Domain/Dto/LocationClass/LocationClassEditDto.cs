using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class LocationClassEditDto
    {
        public LocationClassEditDto()
        {
            // Required for Edit page
        }

        public LocationClassEditDto(LocationClass locClass)
        {
            Active = locClass.Active;
            Name = locClass.Name;
            Description = locClass.Description;
        }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Class")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
