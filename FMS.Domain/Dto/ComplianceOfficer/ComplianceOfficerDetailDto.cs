using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerDetailDto
    {
        public ComplianceOfficerDetailDto(ComplianceOfficer complianceOfficer)
        {
            Id = complianceOfficer.Id;
            Active = complianceOfficer.Active;
            UserId = complianceOfficer.UserId;
            GivenName = complianceOfficer.GivenName;
            FamilyName = complianceOfficer.FamilyName;
            Email = complianceOfficer.Email;
        }

        public Guid Id { get; }
        public bool Active { get; }
        public Guid UserId { get; }
        public string GivenName { get; }
        public string FamilyName { get; }
        public string Email { get; }

        public string Name => $"{FamilyName}, {GivenName}";
        public string DisplayName => $"{GivenName} {FamilyName}";
    }
}