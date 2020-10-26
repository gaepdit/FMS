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
            GivenName = complianceOfficer.GivenName;
            FamilyName = complianceOfficer.FamilyName;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Name => $"{FamilyName}, {GivenName}";

        public string DisplayName => $"{GivenName} {FamilyName}";
    }
}
