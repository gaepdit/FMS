using FMS.Models.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class File : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Internal ID from the Programs, consisting of the 3-digit county number 
        // and a 4-digit system-generated sequential number for each county (xxx-xxxx)
        [StringLength(50)]
        public string FileLabel { get; set; }

        public string FileLocCode { get; set; }

        public string FileLocName { get; set; }

        public List<FileCabinet> Cabinet { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
