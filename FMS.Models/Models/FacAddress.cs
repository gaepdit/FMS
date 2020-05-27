using FMS.Models.Models.Base;

namespace FMS.Models
{
    public class FacAddress : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        public Facility Facility { get; set; }

        public Address SiteAddress { get; set; }

        public Address MailingAddress { get; set; }

        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
