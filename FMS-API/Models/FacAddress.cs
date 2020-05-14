using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacAddress
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid FacAddressID { get; set; }

        public Facility FacilityID { get; set; }

        public Address SiteAddress { get; set; }

        public Address MailingAddress { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
