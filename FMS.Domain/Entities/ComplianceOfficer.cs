using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel, INamedModel
    {
        public ComplianceOfficer() { }

        public ComplianceOfficer(ComplianceOfficerCreateDto complianceOfficer)
        {
            GivenName = complianceOfficer.GivenName;
            FamilyName = complianceOfficer.FamilyName;
            Email = complianceOfficer.Email;
        }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public string Name => string.Join(", ", new[] {FamilyName, GivenName}.Where(s => !string.IsNullOrEmpty(s)));
    }
}