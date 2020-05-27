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
        // Internal ID from the Programs
        [StringLength(50)]
        public string FileID { get; set; }

        public List<EPDProgram> Programs { get; set; }

        public List<EPDUnit> Units { get; set; }

        public List<Budget> FacBudget { get; set; }

        public List<FileLoc> FileLocation { get; set; }

        public List<FileCabinet> Cabinet { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
