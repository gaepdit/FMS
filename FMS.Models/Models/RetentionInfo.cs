using FMS.Models.Models.Base;
using System;

namespace FMS.Models
{
    public class RetentionInfo : BaseActiveModel
    {
        // this class is incomplete until 
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Corresponds to "Accession Number" on 
        public string ConsignmentNumber { get; set; }

        // Not sure if this is necessary
        public string RecordNumber { get; set; }

        // Corresponds to "Item" on Request Form
        public string BoxNumber { get; set; }

        public string OldBoxNumber { get; set; }

        // Corresponds to "Location Number" on Request Form
        public string ShelfNumber { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
