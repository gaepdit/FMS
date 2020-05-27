using FMS.Models.Models.Base;

namespace FMS.Models
{
    public class FacContact : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
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
