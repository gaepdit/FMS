using FMS.Domain.Entities.Base;
using System.Collections.Generic;

namespace FMS.Domain.Entities
{
    public class EnvironmentalInterest : BaseActiveModel
    {
        public string Name { get; set; }

        public List<BudgetCode> BudgetCodes { get; set; }
    }
}