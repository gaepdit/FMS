using FMS.Models.Models.Base;

namespace FMS.Models
{
    public class ComplianceOfficer : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties

        public string Officer { get; set; }

        public EPDProgram Program { get; set; }

        public EPDUnit Unit { get; set; }
        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
