using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class FileCabinet
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid FileCabinetID { get; set; }

        [StringLength(5)]
        public string CabinetNum { get; set; }

        [StringLength(25)]
        public string StartCounty { get; set; }

        [StringLength(25)]
        public string EndCounty { get; set; }

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }

        // Collection of Files in Cabinet go here
        //public Collection<T> 

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
