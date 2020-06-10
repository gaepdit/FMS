using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class FacilityType : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Existing numeric code
        public int FacilityTypeCode { get; set; }

        [StringLength(20)]
        public string FacilityTypeName { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
