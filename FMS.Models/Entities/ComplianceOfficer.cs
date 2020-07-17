using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel
    {
        public string Name { get; set; }

        public OrganizationalUnit Unit { get; set; }
    }
}
