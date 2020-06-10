using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class EPDProgram : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // **** some of these Properties look to used for TMS and may not be necessary ****

        // Internal Program code
        [StringLength(20)]
        public string ProgramCode { get; set; }

        // Internal Program Name
        [StringLength(50)]
        public string ProgramName { get; set; }

        // Internal Program Type
        public char ProgramType { get; set; }

        [StringLength(20)]
        public string Parent { get; set; }

        [StringLength(10)]
        public string Branch { get; set; }

        [StringLength(20)]
        public string OrgNo { get; set; }

        [StringLength(20)]
        public string ProjNo { get; set; }

        // properties from here down (and possibly some above) seem to be for Time Management
        // and may not be necessary
        // Check existing Application and with programs to see what they are used for

        public int OriginCode { get; set; }

        [StringLength(20)]
        public string ProgCode { get; set; }

        [StringLength(20)]
        public string AltID { get; set; }

        [StringLength(5)]
        public string Access { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
