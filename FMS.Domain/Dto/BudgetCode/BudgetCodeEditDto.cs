using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class BudgetCodeEditDto
    {
        public BudgetCodeEditDto()
        {
            // Required for EditBudgetCode page
        }

        public BudgetCodeEditDto(BudgetCode budgetCode)
        {
            Active = budgetCode.Active;
            Code = budgetCode.Code;
            Name = budgetCode.Name;
            OrganizationNumber = budgetCode.OrganizationNumber;
            ProjectNumber = budgetCode.ProjectNumber;
        }

        public bool Active { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Budget Code")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Budget Code Name")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "Organization Number")]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        [Display(Name = "Project Number")]
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