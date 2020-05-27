using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class FacType : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Existing numeric code
        public int FacTypeCode { get; set; }

        [StringLength(20)]
        public string FacTypeName { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
