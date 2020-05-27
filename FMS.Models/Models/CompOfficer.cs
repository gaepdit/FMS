using FMS.Models.Models.Base;

namespace FMS.Models
{
    public class CompOfficer : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties

        public User UserObj { get; set; }

        public EPDProgram Program { get; set; }

        public EPDUnit Unit { get; set; }
        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
