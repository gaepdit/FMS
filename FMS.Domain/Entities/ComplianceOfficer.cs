using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel
    {
        [Display(Name = "Compliance Officer")]
        public string Name { get; set; }

        public OrganizationalUnit Unit { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
