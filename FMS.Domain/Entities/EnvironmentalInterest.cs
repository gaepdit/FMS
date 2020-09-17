using FMS.Domain.Entities.Base;
using System.Collections.Generic;

namespace FMS.Domain.Entities
{
    public class EnvironmentalInterest : BaseActiveNamedModel
    {
        //[Display(Name = "Environmental Interest")]
        //public string Name { get; set; }

        public ICollection<BudgetCode> BudgetCodes { get; set; }
    }
}