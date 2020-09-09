using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetDetailDto
    {
        public CabinetDetailDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            Name = cabinet.Name;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }
    }
}
