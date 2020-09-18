using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EnvironmentalInterestEditDto
    {
        public EnvironmentalInterestEditDto() { }

        public EnvironmentalInterestEditDto(EnvironmentalInterest environmentalInterest)
        {
            Id = environmentalInterest.Id;
            Active = environmentalInterest.Active;
            Name = environmentalInterest.Name;
            //BudgetCode = environmentalInterest.BudgetCode.Id;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Environmental Interest")]
        public string Name { get; set; }

        //public Guid BudgetCodeId { get; set; }
    }
}
