using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FundingSourceSummaryDto
    {
        public FundingSourceSummaryDto(FundingSource fundingSource)
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
