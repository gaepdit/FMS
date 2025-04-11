﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class ContactTypeCreateDto
    {
        [Display(Name = "Contact Type")]
        public string Type { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
