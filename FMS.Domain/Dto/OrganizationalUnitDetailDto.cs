using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitDetailDto
    {
        public OrganizationalUnitDetailDto(OrganizationalUnit organizationalUnit)
        {
            Id = organizationalUnit.Id;
            Active = organizationalUnit.Active;
            Name = organizationalUnit.Name;
            //ComplianceOfficerId = organizationalUnit.ComplianceOfficer.Id;
        }

        public Guid Id;

        public bool Active { get; set; }

        [StringLength(20)]
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        //public Guid ComplianceOfficerId { get; set; }
    }
}
