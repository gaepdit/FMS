using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class File
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public string FileID { get; set; }

        public EPDProgram Program { get; set; }

        public County Cnty { get; set; }

        public FacAddress Address { get; set; }

        public FacLatLong Coords { get; set; }

        public string Status { get; set; }

        public EPDUnit Unit { get; set; }

        public Budget FacBudget { get; set; }

        public FileLoc FileLocation { get; set; }

        public FileCabinet Cabinet { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
