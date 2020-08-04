using FMS.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class EnvironmentalInterest : BaseActiveModel
    {
        [Display(Name = "Environmental Program")]
        public string Name { get; set; }

        public List<BudgetCode> BudgetCodes { get; set; }

    }
}