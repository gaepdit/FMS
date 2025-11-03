using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AbandonedInactiveSummaryDto
    {
        public AbandonedInactiveSummaryDto(AbandonedInactive abandonedInactive)
        {
            Id = abandonedInactive.Id;
            Active = abandonedInactive.Active;
            Name = abandonedInactive.Name;
            Description = abandonedInactive.Description;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "Pertinent Information for Aban/Inac Sites")]
        public string Name { get; }

        public string Description { get; }
    }
}
