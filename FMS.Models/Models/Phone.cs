using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class Phone : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
      
        // Cell, office, etc., Maybe Use Enum or DB Table???
        public int NumberType {get; set;}

        [Phone]
        public string Telephone { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
