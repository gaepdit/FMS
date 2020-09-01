﻿using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerSpec
    {
        public bool? Active { get; set; }

        [Display(Name = "Compliance Officer")]
        public string Name { get; set; }

        //public Guid? OrganizationalUnitId { get; set; }
    }
}
