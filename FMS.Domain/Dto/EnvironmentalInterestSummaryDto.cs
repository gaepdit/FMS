using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class EnvironmentalInterestSummaryDto
    {
        public EnvironmentalInterestSummaryDto(EnvironmentalInterest environmentalInterest)
        {
            Id = environmentalInterest.Id;
            Active = environmentalInterest.Active;
            Name = environmentalInterest.Name;
            //BudgetCode = environmentalInterest.BudgetCode.Id;
        }

        public Guid Id;

        public bool? Active { get; set; }

        [Display(Name = "Environmental Interest")]
        public string Name { get; set; }

        //public Guid BudgetCodeId { get; set; }
    }
}
