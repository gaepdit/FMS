using FMS.Domain.Entities.Base;
using System.Collections.Generic;

namespace FMS.Domain.Entities
{
    public class OrganizationalUnit : BaseActiveNamedModel
    {
        //[Display(Name = "Organizational Unit")]
        //public string Name { get; set; }

        public List<ComplianceOfficer> ComplianceOfficers { get; set; }
    }
}
