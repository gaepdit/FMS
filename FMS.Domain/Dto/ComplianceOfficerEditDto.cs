using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerEditDto
    {
        public ComplianceOfficerEditDto() { }

        public ComplianceOfficerEditDto(ComplianceOfficer complianceOfficer)
        {
            Id = complianceOfficer.Id;
            Active = complianceOfficer.Active;
            FirstName = complianceOfficer.FirstName;
            LastName = complianceOfficer.LastName;
            //OrganizationalUnitId = complianceOfficer.OrganizationalUnit.Id;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public Guid OrganizationalUnitId { get; set; }
    }
}
