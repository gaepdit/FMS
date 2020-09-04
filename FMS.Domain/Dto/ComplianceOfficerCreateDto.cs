using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Compliance Officer")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public Guid OrganizationalUnitId { get; set; }
    }
}
