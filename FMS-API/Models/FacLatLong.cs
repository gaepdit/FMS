using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacLatLong
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public long FacID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
