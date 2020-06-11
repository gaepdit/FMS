using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class EPDUnit : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties

        // Existing ID for the Unit
        public int UnitNumber { get; set; }

        [StringLength(50)]
        public string UnitName { get; set; }

        #endregion

        #region Methods
        //Methods here
       
        #endregion
    }
}
