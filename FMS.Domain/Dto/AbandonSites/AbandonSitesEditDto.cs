using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AbandonSitesEditDto
    {
        public AbandonSitesEditDto()
        {
            // Required for EditAbandonSites page
        }

        public AbandonSitesEditDto(AbandonSites abandonSites)
        {
            Id = abandonSites.Id;
            Active = abandonSites.Active;
            Name = abandonSites.Name;
            Description = abandonSites.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Abandon Site Name is required.")]
        [Display(Name = "Abandon Site")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string GetName()
        {
            return $"{Name} - ({Description})";
        }
    }
}
