using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel, INamedModel
    {
        [Display(Name = "Compliance Officer")]
        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        //public Guid UnitId { get; set; }
        public OrganizationalUnit Unit { get; set; }   //virtual

        public string Name => string.Join(", ", new[] { FamilyName, GivenName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
