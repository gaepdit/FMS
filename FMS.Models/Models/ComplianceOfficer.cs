using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class ComplianceOfficer : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unit and Program for the Compliance officer may be different
        // from the Facilities that they are assigned. 
        [StringLength(50)]
        public string OfficerName { get; set; }

        public EPDProgram Program { get; set; }

        public EPDUnit Unit { get; set; }
        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
