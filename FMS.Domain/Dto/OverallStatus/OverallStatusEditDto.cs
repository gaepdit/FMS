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
            Description = overallStatus.Description;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Overall Status")]
        [Required(ErrorMessage = "Overall Status is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string DisplayName => $"{Name} ({Description})";
    }
}
