using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerSpec
    {
        public bool Active { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }


        //public Guid? OrganizationalUnitId { get; set; }
    }
}
