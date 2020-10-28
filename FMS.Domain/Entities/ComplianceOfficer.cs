using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel, INamedModel
    {
        public ComplianceOfficer() { }

        public ComplianceOfficer(ComplianceOfficerCreateDto complianceOfficerCreateDto)
        {
            Active = complianceOfficerCreateDto.Active;
            GivenName = complianceOfficerCreateDto.GivenName;
            FamilyName = complianceOfficerCreateDto.FamilyName;
        }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public OrganizationalUnit Unit { get; set; }   //virtual

        public string Name => string.Join(", ", new[] { FamilyName, GivenName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
