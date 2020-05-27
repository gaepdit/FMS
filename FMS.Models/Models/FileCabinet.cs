using FMS.Models.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class FileCabinet : BaseActiveModel
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Existing Program Cabinet Numbers
        [StringLength(5)]
        public string CabinetNum { get; set; }

        public County StartCounty { get; set; }

        public County EndCounty { get; set; }

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }

        // Collection of Files in Cabinet go here if necessary

        #endregion

        #region Methods
        //Methods here

        #endregion
    }
}
