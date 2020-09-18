using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitSummaryDto
    {
        public OrganizationalUnitSummaryDto(OrganizationalUnit organizationalUnit)
        {
            Id = organizationalUnit.Id;
            Active = organizationalUnit.Active;
            Name = organizationalUnit.Name;
            //ComplianceOfficerId = organizationalUnit.ComplianceOfficer.Id;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Facility Type")]
        public string Name { get; set; }

        //public Guid ComplianceOfficerId { get; set; }
    }
}
