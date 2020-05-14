using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class User
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid UserID { get; set; }

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

        public int SecurityRole { get; set; }

        public bool Active { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
