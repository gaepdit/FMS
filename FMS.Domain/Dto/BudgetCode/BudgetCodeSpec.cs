using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class BudgetCodeSpec
    {
        public Guid? Id { get; set; }

        public bool Active { get; set; }

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
