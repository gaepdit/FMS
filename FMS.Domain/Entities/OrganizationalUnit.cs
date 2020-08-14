using FMS.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class OrganizationalUnit : BaseActiveModel
    {
        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

        public List<ComplianceOfficer> ComplianceOfficers { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
