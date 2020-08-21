using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class BudgetCode : BaseActiveModel
    {
        // Internal Programs Budget code
        public EnvironmentalInterest EnvironmentalInterest { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public string Name { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }
    }
}
