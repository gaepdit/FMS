using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetSummaryDto
    {
        public CabinetSummaryDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            Name = cabinet.Name;
        }

        public Guid Id { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; set; }
    }
}
