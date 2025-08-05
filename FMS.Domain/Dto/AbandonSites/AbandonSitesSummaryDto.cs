using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class AbandonSitesSummaryDto
    {
        public AbandonSitesSummaryDto(AbandonSites abandonSites)
        {
            Id = abandonSites.Id;
            Active = abandonSites.Active;
            Name = abandonSites.Name;
            Description = abandonSites.Description;
        }
        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Abandon Site")]
        public string Name { get; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
