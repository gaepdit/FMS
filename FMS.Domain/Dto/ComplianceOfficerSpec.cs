using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerSpec
    {
        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        //public Guid? OrganizationalUnitId { get; set; }
    }
}
