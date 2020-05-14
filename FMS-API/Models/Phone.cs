using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class Phone
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid PhoneID { get; set; }

        // Cell, office, etc.
        public PhoneType NumberType {get; set;}

        public int CountryCode { get; set; } 

        public int AreaCode { get; set; }

        public int Prefix { get; set; }

        public int Number { get; set; }

        #endregion

        #region Methods
        //Methods here
        public string GetPhoneNumber()
        {
            return "(" + AreaCode + ")" + Prefix + "-" + Number;
        }

        public void SetPhoneNumber(int areaCode, int prefix, int number)
        {
            AreaCode = areaCode;
            Prefix = prefix;
            Number = number;
        }
        #endregion
    }
}
