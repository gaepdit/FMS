using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class BudgetCodeSummaryDto
    {
        public BudgetCodeSummaryDto(BudgetCode budgetCode)
        {
            Id = budgetCode.Id;
            Active = budgetCode.Active;
            Code = budgetCode.Code;
            Name = budgetCode.Name;
            OrganizationNumber = budgetCode.OrganizationNumber;
            ProjectNumber = budgetCode.ProjectNumber;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        //public Guid? EnvironmentalInterestId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "Budget Code")]
        public string Name { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }
    }
}
