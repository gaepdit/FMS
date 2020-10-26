using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class BudgetCode : BaseActiveNamedModel
    {
        public BudgetCode() { }

        public BudgetCode(BudgetCodeCreateDto newBudgetCode)
        {
            Name = newBudgetCode.Name;
            Code = newBudgetCode.Code;
            OrganizationNumber = newBudgetCode.OrganizationNumber;
            ProjectNumber = newBudgetCode.ProjectNumber;
        }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }
    }
}
