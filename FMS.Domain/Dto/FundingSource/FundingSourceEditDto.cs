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
            Active = fundingSource.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Funding Source")]
        public string Name { get; set; }
    }
}
