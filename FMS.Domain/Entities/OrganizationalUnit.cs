using FMS.Domain.Entities.Base;
using System.Collections.Generic;

namespace FMS.Domain.Entities
{
    public class OrganizationalUnit : BaseActiveModel
    {
        public string Name { get; set; }

        public List<ComplianceOfficer> ComplianceOfficers { get; set; }
    }
}
