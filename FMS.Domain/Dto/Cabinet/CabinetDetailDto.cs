using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Dto
{
    public class CabinetDetailDto
    {
        public CabinetDetailDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            Name = cabinet.Name;
            Files = cabinet.CabinetFiles?
                .Select(c => new FileSummaryDto(c.File)).ToList()
                ?? new List<FileSummaryDto>();
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; }

        public List<FileSummaryDto> Files { get; }
    }
}
