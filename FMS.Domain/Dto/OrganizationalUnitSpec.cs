using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitSpec
    {
        public bool? Active { get; set; }

        [Display(Name = "Organizational Unit")]
        public string? Name { get; set; }

        //public Guid? ComplianceOfficerId { get; set; }
    }
}
