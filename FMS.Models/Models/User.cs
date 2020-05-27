using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class User : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
       
        [StringLength(25)]
        public string UserName { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [EmailAddress]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string LoginEmail { get; set; }

        [StringLength(25)]
        public string ProgramName { get; set; }

        // Use Enum or Table??? Int or String???
        public int SecurityRole { get; set; }

        #endregion

        #region Methods
        //Methods here
        
        #endregion
    }
}
