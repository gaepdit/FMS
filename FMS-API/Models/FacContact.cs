using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FacContact
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid FacContactID { get; set; }

        // Contact's Facility
        public Facility Facility { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public FacAddress CompanyAddress { get; set; }

        public Email ContactEmail { get; set; }

        public Phone CompanyPhone { get; set; }

        public Phone ContactPhone { get; set; }

        #endregion

        #region Methods
            //Methods here
        #endregion
    }
}
