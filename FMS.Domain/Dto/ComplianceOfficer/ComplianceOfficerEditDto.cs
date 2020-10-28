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
            GivenName = complianceOfficer.GivenName;
            FamilyName = complianceOfficer.FamilyName;
            Email = complianceOfficer.Email;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }
    }
}
