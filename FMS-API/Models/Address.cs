using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class Address
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid AddressID { get; set; }

        [StringLength(50)]
        public string StreetLine1 {get; set;}

        [StringLength(50)]
        public string StreetLine2 { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        public County CountyCode { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public int PostalCode { get; set; }

        public bool Active { get; set; }
        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
