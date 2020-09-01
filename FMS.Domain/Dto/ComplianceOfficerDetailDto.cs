using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerDetailDto
    {
        public ComplianceOfficerDetailDto(ComplianceOfficer complianceOfficer)
        {
            Id = complianceOfficer.Id;
            Active = complianceOfficer.Active;
            Name = complianceOfficer.Name;
            //OrganizationalUnitId = complianceOfficer.OrganizationalUnit.Id;
        }

        public Guid Id;

        public bool Active { get; set; }

        [Display(Name = "Budget Code")]
        public string Name { get; set; }

        //public Guid OrganizationalUnitId { get; set; }
    }
}
