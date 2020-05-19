using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FMS_API.Models
{
    public class County
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public int CountyID { get; set; }

        [StringLength(20)]
        [DisplayFormat(
            NullDisplayText = FMS.NotEnteredDisplayText,
            ConvertEmptyStringToNull = true)]
        public string CountyName { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
