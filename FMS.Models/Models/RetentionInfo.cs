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

        // Corresponds to "Accession Number" on RC spreadsheet
        public string ConsignmentNumber { get; set; }

        // Corresponds to "Item" on Request Form
        public string BoxNumber { get; set; }

        // Corresponds to "Location Number" on Request Form
        public string ShelfNumber { get; set; }

        // Retention Schedule Number DDDD-DDDD
        public string RetentionSchedule { get; set; }

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
