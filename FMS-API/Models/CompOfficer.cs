using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class CompOfficer
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public User UserObj { get; set; }

        public EPDProgram Program { get; set; }

        public EPDUnit Unit { get; set; }
        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
