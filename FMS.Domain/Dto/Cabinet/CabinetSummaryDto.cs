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
            CabinetNumber = cabinet.CabinetNumber;
            Name = cabinet.Name;
            FirstFileLabel = cabinet.FirstFileLabel;
        }

        public Guid Id { get; }
        public bool Active { get; }
        public int CabinetNumber { get; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; }
        
        [Display(Name = "First File Label")]
        public string FirstFileLabel { get;  }
    }
}
