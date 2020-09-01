﻿using FMS.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class BudgetCode : BaseActiveModel
    {
        //public Guid EnvironmentalInterestId { get; set; }
        public  EnvironmentalInterest EnvironmentalInterest { get; set; }   //virtual

        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "Budget Code")]
        public string Name { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }
    }
}
