﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models.Models
{
    public class File : BaseActiveModel
    {
        // Internal ID from the Programs, consisting of the 3-digit county number 
        // and a 4-digit system-generated sequential number for each county (xxx-xxxx)
        [StringLength(9)]
        public string FileLabel { get; set; }

        // public string FileLocCode { get; set; }

        // public string FileLocName { get; set; }

        public List<FileCabinet> FileCabinets { get; set; }

        public List<Facility> Facilities { get; set; }
    }
}
