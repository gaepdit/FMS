using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacType
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid FacTypeID { get; set; }

        public string FacTypeCode { get; set; }

        public string FacTypeName { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
