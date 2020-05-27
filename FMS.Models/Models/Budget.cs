using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class Budget : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Internal Programs Budget code
        [StringLength(20)]
        public string BudgetCode { get; set; }

        [StringLength(50)]
        public string BudgetCodeName { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
