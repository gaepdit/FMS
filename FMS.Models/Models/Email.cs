using FMS.Models.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
using System.Threading.Tasks;

namespace FMS.Models
{
    public class Email : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties

        public User User { get; set; }

        public string EmailAddress { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
