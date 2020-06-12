using System.Collections.Generic;

namespace FMS.Models.Models
{
    public class EnvironmentalInterest : BaseActiveModel
    {
        public string name { get; set; }

        public List<BudgetCode> BudgetCodes { get; set; }

    }
}