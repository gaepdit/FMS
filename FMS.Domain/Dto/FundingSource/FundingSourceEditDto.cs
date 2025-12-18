using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FundingSourceEditDto
    {
        public FundingSourceEditDto() { }

        public FundingSourceEditDto(FundingSource fundingSource)
        {
            Id = fundingSource.Id;
            Name = fundingSource.Name;
            Description = fundingSource.Description;
            Active = fundingSource.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Funding Source")]
        [Required(ErrorMessage = "Funding Source is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        //public string DisplayName => $"{Name} ({Description})";
    }
}
