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

        public Guid Id { get; }
        public bool Active { get; }
        
        [Display(Name = "Budget Code")]
        public string Code { get; }

        [Display(Name = "Budget Code Name")]
        public string Name { get; }

        [Display(Name = "Organization Number")]
        public string OrganizationNumber { get; }
        
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; }
    }
}
