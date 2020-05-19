using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class Budget
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public string BudgetCode { get; set; }

        public string BudgetCodeName { get; set; }
            
        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
