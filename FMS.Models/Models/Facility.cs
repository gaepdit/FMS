using FMS.Models.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class Facility : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Existing ID for Facility May be used by Programs
        public string FacilityID { get; set; }

        // Facility Name
        [StringLength(50)]
        public string Name { get; set;}

        // Facility contact
        public FacContact Contact { get; set; }

        // Facility Address
        public FacAddress SiteAddress { get; set; }

        // site Coordinates
        public FacLatLong LatLong { get; set; }

        // County for use to simplify queries, ease searching, etc.
        public County County { get; set; }

        // Facility Contact Email
        public Email Email { get; set; }

        // Facility Contact Phone
        public Phone Phone { get; set; }

        // List File Cabinets that contain this Facility File
        public List<FileCabinet> Cabinets {get; set;}

        // List of Files that contain this Facility
        public List<File> Files { get; set; }

        // List of Permits, Environmental Interests, Programs, Etc. at this Facility
        public List<EPDProgram> EnvPrograms { get; set; }
        #endregion

        #region Methods
        //Methods here
       
        #endregion
    }
}
