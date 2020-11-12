using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class CabinetDetailDto
    {
        public CabinetDetailDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            Name = cabinet.Name;
            FirstFileLabel = cabinet.FirstFileLabel;
            Facilities = new List<FacilitySummaryDto>();
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; }

        [Display(Name = "First File Label")]
        public string FirstFileLabel { get; }

        public List<FacilitySummaryDto> Facilities { get; }
    }
}