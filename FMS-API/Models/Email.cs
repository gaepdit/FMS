﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class Email
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        public User UserObj { get; set; }

        public Email EmailObj { get; set; }

        #endregion

        #region Methods
        //Methods here
        #endregion
    }
}
