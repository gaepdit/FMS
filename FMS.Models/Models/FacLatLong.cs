using FMS.Models.Models.Base;
using System;

namespace FMS.Models
{
    public class FacLatLong : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
            // The Id of the Facility
        public Facility Facility { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
