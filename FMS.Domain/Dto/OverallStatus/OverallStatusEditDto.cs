using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OverallStatusEditDto
    {
        public OverallStatusEditDto() { }

        public OverallStatusEditDto(OverallStatus overallStatus)
        {
            Id = overallStatus.Id;
            Active = overallStatus.Active;
            Name = overallStatus.Name;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Overall Status")]
        public string Name { get; set; }
    }
}
