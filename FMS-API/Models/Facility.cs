using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class Facility
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        // Simple Number ID for Facility
        public long FacID { get; set; }

        // Facility Name
        public string Name { get; set;}

        // Facility contact
        public FacContact Contact { get; set; }

        // Facility Address
        public FacAddress SiteAddress { get; set; }

        // site Coordinates
        public FacLatLong LatLong { get; set; }

        // Facility Contact Email
        public Email Email { get; set; }

        // Facility Contact Phone
        public Phone Phone { get; set; }

        // List File Cabinets that contain this Facility File
        public ArrayList Cabinets = new ArrayList();

        // List of Files that contain this Facility
        public ArrayList Files = new ArrayList();

        // List of Permits, Environmental Interests, Programs, Etc. at this Facility
        public ArrayList EnvPrograms = new ArrayList();
        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
