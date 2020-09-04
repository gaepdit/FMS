using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel
    {
        [Display(Name = "Compliance Officer")]
        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        //public Guid UnitId { get; set; }
        public  OrganizationalUnit Unit { get; set; }   //virtual
    }
}
