using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FMS.Models.Models.Base;
//using System.Text.Json;

namespace FMS.Models
{
    public class Address : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        [StringLength(50)]
        public string StreetLine1 { get; set; }

        [StringLength(50)]
        public string StreetLine2 { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; } = "GA";

        [StringLength(10)]
        public int PostalCode { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
