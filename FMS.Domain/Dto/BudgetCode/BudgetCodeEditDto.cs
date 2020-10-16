using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class BudgetCodeEditDto
    {
        public BudgetCodeEditDto() { }

        public BudgetCodeEditDto(BudgetCode budgetCode)
        {
            Active = budgetCode.Active;
            Code = budgetCode.Code;
            Name = budgetCode.Name;
            OrganizationNumber = budgetCode.OrganizationNumber;
            ProjectNumber = budgetCode.ProjectNumber;
        }

        public bool Active { get; set; }

        [StringLength(20)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Budget Code")]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }

        public void TrimAll()
        {
            Code = Code?.Trim();
            Name = Name?.Trim();
            OrganizationNumber = OrganizationNumber?.Trim();
            ProjectNumber = ProjectNumber?.Trim();
        }
    }
}
