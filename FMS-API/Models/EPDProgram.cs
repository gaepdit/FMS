using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class EPDProgram
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public string ProgramCode { get; set; }

        public string ProgramName { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
