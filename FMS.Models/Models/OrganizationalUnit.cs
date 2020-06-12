using System.Collections.Generic;

namespace FMS.Models.Models
{
    public class OrganizationalUnit : BaseActiveModel
    {
        public string Name { get; set; }

        public List<ComplianceOfficer> ComplianceOfficers { get; set; }
    }
}
